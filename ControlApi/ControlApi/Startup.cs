using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedLibrary.Extensions;
using SharedLibrary.Configuration.Tenancy;
using SharedLibrary.Configuration.PayPal;
using SharedLibrary.Configuration.FacePlusPlus;
using BusinessLibrary.SignalR;

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

            // DataAccesLibray Stores
            services.AddTransient(typeof(DataAccessLibrary.Stores.IStore<>), typeof(DataAccessLibrary.Stores.StoresImplementations.Store<>));
            services.AddTransient(typeof(DataAccessLibrary.Stores.IStoreByBuilding<>), typeof(DataAccessLibrary.Stores.StoresImplementations.StoreByBuilding<>));

            services.AddSingleton<SharedLibrary.Configuration.FacePlusPlus.FacePlusPlus>();

            services.AddSignalR();
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
