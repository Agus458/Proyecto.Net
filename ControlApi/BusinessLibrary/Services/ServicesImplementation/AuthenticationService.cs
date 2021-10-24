﻿using SharedLibrary.Configuration;
using DataAccessLibrary.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SharedLibrary.DataTypes.Authentication;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SharedLibrary.Error;
using System.Net;

namespace BusinessLibrary.Services.ServicesImplementation
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> UserManager;

        private readonly JwtTokenConfiguration Configuration;

        private readonly HttpContext HttpContext;

        public AuthenticationService(UserManager<User> UserManager, JwtTokenConfiguration Configuration, IHttpContextAccessor HttpContextAccessor)
        {
            this.UserManager = UserManager;
            this.Configuration = Configuration;
            this.HttpContext = HttpContextAccessor.HttpContext;
        }

        public async Task<dynamic> Login(LoginRequestDataType Data)
        {
            if (Data == null) throw new ArgumentNullException();

            Tenant Aux = null;

            if (HttpContext.Items.TryGetValue(ApiConstants.HttpContextTenant, out var Tenant))
            {
                Aux = Tenant as Tenant;
            }

            var User = await this.UserManager.FindByEmailAsync(Data.Email);

            if (User != null)
            {
                if (await this.UserManager.CheckPasswordAsync(User, Data.Password))
                {
                    var Roles = await this.UserManager.GetRolesAsync(User);

                    if (!await this.UserManager.IsInRoleAsync(User, "SuperAdmin"))
                    {
                        if (Tenant == null || User.TenantId != Aux.Id)
                        {
                            throw new ApiError("Unautorized", (int)HttpStatusCode.Unauthorized);
                        }
                    }

                    return this.GenerateToken(User, Roles, Aux?.SocialReason);
                }

                throw new ApiError("Incorrect Password", (int)HttpStatusCode.BadRequest);
            }

            throw new ApiError("User Not Found", (int)HttpStatusCode.NotFound);
        }

        private dynamic GenerateToken(User User, IList<string> Roles, string SocialReason)
        {
            // Payload for the token.
            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, User.Email),
                new Claim(ClaimTypes.NameIdentifier, User.Id.ToString()),
                new Claim("Tenant", SocialReason ?? ""),
            };

            foreach (var Role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, Role));
            }

            var JwtTokenHandler = new JwtSecurityTokenHandler();

            // Here we define the content that the token will have.
            var TokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(Claims),
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(Configuration.Key, SecurityAlgorithms.HmacSha256)
            };

            // Creates the authorization token with the description we have created before
            var Token = JwtTokenHandler.CreateToken(TokenDescription);
            var JwtToken = JwtTokenHandler.WriteToken(Token);

            return new { Token = JwtToken, User.Email, Roles, Tenant = SocialReason };
        }
    }
}
