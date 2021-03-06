using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using SharedLibrary.Extensions;
using SharedLibrary.Configuration.Tenancy;
using BusinessLibrary.SignalR;
using System.Text.Json.Serialization;
using Quartz;
using ControlApi.Jobs;

namespace ControlApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddApiConfiguration(Configuration);

            services.AddCors(options =>
            {
                options.AddPolicy(name: "FrontEndOrigin",
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                                  });
            });

            services.AddHttpClient();

            services.AddAutoMapper();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ControlApi", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \n\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \n\n Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new string[] {}
                    }
                });

                c.OperationFilter<SwaggerTenantFilter>();
            });

            // Here is defined the connection to the sql server database.
            services.AddDbContext<DataAccessLibrary.Contexts.ApiDbContext>(Options =>
            {
                Options.UseSqlServer(Configuration.GetConnectionString("DefaultDbConnection"));
            });

            // Here is defined the user identity class and the context that will use.
            services.AddIdentity<DataAccessLibrary.Entities.User, IdentityRole>(Options =>
            {
                Options.Password.RequiredLength = 0;
                Options.Password.RequireNonAlphanumeric = false;
                Options.Password.RequireUppercase = false;
                Options.Password.RequireDigit = false;
                Options.Password.RequireLowercase = false;
                Options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<DataAccessLibrary.Contexts.ApiDbContext>();

            // Here is defined the key that is going to be used to generate JwtTokens.
            var JwtBearerConfiguration = new SharedLibrary.Configuration.JwtTokenConfiguration(Configuration.GetValue<string>("JwtSecretKey"));
            services.AddSingleton(JwtBearerConfiguration);

            services.AddAuthentication(Options =>
            {
                Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtOptions =>
            {
                JwtOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = JwtBearerConfiguration.Key,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5)
                };
            });

            services.AddMultitenancy();

            // Here we define all de dependency injection needed.
            // Buiseness library Services
            services.AddTransient<BusinessLibrary.Services.IAuthenticationService, BusinessLibrary.Services.ServicesImplementation.AuthenticationService>();
            services.AddTransient<BusinessLibrary.Services.IUsersService, BusinessLibrary.Services.ServicesImplementation.UsersService>();
            services.AddTransient<BusinessLibrary.Services.ITenantsService, BusinessLibrary.Services.ServicesImplementation.TenantsService>();
            services.AddTransient<BusinessLibrary.Services.IDoorsService, BusinessLibrary.Services.ServicesImplementation.DoorsService>();
            services.AddTransient<BusinessLibrary.Services.IPersonsService, BusinessLibrary.Services.ServicesImplementation.PersonsService>();
            services.AddTransient<BusinessLibrary.Services.IBuildingsService, BusinessLibrary.Services.ServicesImplementation.BuildingsService>();
            services.AddTransient<BusinessLibrary.Services.INoveltyService, BusinessLibrary.Services.ServicesImplementation.NoveltyService>();
            services.AddTransient<BusinessLibrary.Services.IAssignmentsService, BusinessLibrary.Services.ServicesImplementation.AssignmentsService>();
            services.AddTransient<BusinessLibrary.Services.INotificationService, BusinessLibrary.Services.ServicesImplementation.NotificationService>();
            services.AddTransient<BusinessLibrary.Services.IEventsService, BusinessLibrary.Services.ServicesImplementation.EventsService>();
            services.AddTransient<BusinessLibrary.Services.IEntriesService, BusinessLibrary.Services.ServicesImplementation.EntriesService>();
            services.AddTransient<BusinessLibrary.Services.IRoomsService, BusinessLibrary.Services.ServicesImplementation.RoomsService>();

            services.AddTransient<BusinessLibrary.Services.IFacturaService, BusinessLibrary.Services.ServicesImplementation.FacturaService>();
            services.AddTransient<BusinessLibrary.Services.IPagoService, BusinessLibrary.Services.ServicesImplementation.PagoService>();
            services.AddTransient<BusinessLibrary.Services.IPrecioService, BusinessLibrary.Services.ServicesImplementation.PrecioService>();
            services.AddTransient<BusinessLibrary.Services.IProductsService, BusinessLibrary.Services.ServicesImplementation.ProductsService>();

            // DataAccesLibray Stores
            services.AddTransient(typeof(DataAccessLibrary.Stores.IStore<>), typeof(DataAccessLibrary.Stores.StoresImplementations.Store<>));
            services.AddTransient(typeof(DataAccessLibrary.Stores.IStoreByBuilding<>), typeof(DataAccessLibrary.Stores.StoresImplementations.StoreByBuilding<>));
            services.AddTransient<DataAccessLibrary.Stores.IEventsStore, DataAccessLibrary.Stores.StoresImplementations.EventsStore>();
            services.AddTransient<DataAccessLibrary.Stores.IAssignmentsStore, DataAccessLibrary.Stores.StoresImplementations.AssignmentsStore>();
            services.AddTransient<DataAccessLibrary.Stores.INotificationStore, DataAccessLibrary.Stores.StoresImplementations.NotificationStore>();
            services.AddTransient<DataAccessLibrary.Stores.INoveltyStore, DataAccessLibrary.Stores.StoresImplementations.NoveltyStore>();

            services.AddTransient<DataAccessLibrary.Stores.IPagoStore, DataAccessLibrary.Stores.StoresImplementations.PagoStore>();
            services.AddTransient<DataAccessLibrary.Stores.IPrecioStore, DataAccessLibrary.Stores.StoresImplementations.PrecioStore>();
            services.AddTransient<DataAccessLibrary.Stores.IProductsStore, DataAccessLibrary.Stores.StoresImplementations.ProductStore>();

            services.AddSingleton<SharedLibrary.Configuration.FacePlusPlus.FacePlusPlus>();

            services.AddSignalR();

            // Add the required Quartz.NET services
            services.AddQuartz(q =>
            {
                // Use a Scoped container to create jobs. I'll touch on this later
                q.UseMicrosoftDependencyInjectionScopedJobFactory();

                // Create a "key" for the job
                var jobKey = new JobKey("MonthlyBillsJob");

                // Register the job with the DI container
                q.AddJob<MonthlyBillsJob>(opts => opts.WithIdentity(jobKey));

                // Create a trigger for the job
                q.AddTrigger(opts => opts
                    .ForJob(jobKey) // link to the HelloWorldJob
                    .WithIdentity("MonthlyBillsJob-trigger") // give the trigger a unique name
                                                             //.WithCronSchedule("0 0/1 * * * ?")); // run every minute
                    .WithCronSchedule("0 0 0 1 * ?")); // run every first of month
            });

            // Add the Quartz.NET hosted service
            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<DataAccessLibrary.Entities.User> UserManager, RoleManager<IdentityRole> RoleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ControlApi v1"));
            }

            app.ConfigureApiExceptionMiddleware();

            DataAccessLibrary.Contexts.DataInitialization.SeedAsync(UserManager, RoleManager).Wait();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("FrontEndOrigin");

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseTenancy();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<BroadcastHub>("/Notify");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
