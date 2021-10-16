using BusinessLibrary.Configuration;
using BusinessLibrary.Services;
using BusinessLibrary.Services.ServicesImplementation;
using DataAccessLibrary.Contexts;
using DataAccessLibrary.Entities;
using DataAccessLibrary.Stores;
using DataAccessLibrary.Stores.StoresImplementations;
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ControlApi", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
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
                            }
                        },
                        new string[] {}
                    }
                });
            });

            // Here is defined the connection to the sql server database.
            services.AddDbContext<ApiDbContext>(Options =>
            {
                Options.UseSqlServer(Configuration.GetConnectionString("DefaultDbConnection"));
            });

            // Here is defined the user identity class and the context that will use.
            services.AddIdentity<User, IdentityRole>(Options =>
            {
                Options.Password.RequiredLength = 0;
                Options.Password.RequireNonAlphanumeric = false;
                Options.Password.RequireUppercase = false;
                Options.Password.RequireDigit = false;
                Options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ApiDbContext>();

            // Here is defined the key that is going to be used to generate JwtTokens.
            var JwtBearerConfiguration = new JwtTokenConfiguration(Configuration.GetValue<string>("JwtSecretKey"));
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



            // Here we define all de dependency injection needed.
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<ITenantsStore, TenantsStore>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<User> UserManager, RoleManager<IdentityRole> RoleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ControlApi v1"));
            }

            DataInitialization.SeedAsync(UserManager, RoleManager).Wait();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
