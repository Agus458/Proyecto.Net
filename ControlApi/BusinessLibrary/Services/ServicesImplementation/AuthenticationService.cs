using SharedLibrary.Configuration;
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
using SharedLibrary.Extensions;
using DataAccessLibrary.Stores;

namespace BusinessLibrary.Services.ServicesImplementation
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> UserManager;

        private readonly JwtTokenConfiguration Configuration;

        private readonly HttpContext HttpContext;

        private readonly ITenantsStore TenantsStore;

        public AuthenticationService(UserManager<User> UserManager, JwtTokenConfiguration Configuration, IHttpContextAccessor HttpContextAccessor, ITenantsStore TenantsStore)
        {
            this.UserManager = UserManager;
            this.Configuration = Configuration;
            this.HttpContext = HttpContextAccessor.HttpContext;
            this.TenantsStore = TenantsStore;
        }

        public async Task<dynamic> Login(LoginRequestDataType Data)
        {
            if (Data == null) throw new ArgumentNullException();

            var User = await this.UserManager.FindByEmailAsync(Data.Email);

            if (User != null)
            {
                if (await this.UserManager.CheckPasswordAsync(User, Data.Password))
                {
                    var Roles = await this.UserManager.GetRolesAsync(User);

                    Tenant Tenant = null;

                    if (Data.SocialReason != null) Tenant = this.TenantsStore.GetBySocialReason(Data.SocialReason);

                    if (!await this.UserManager.IsInRoleAsync(User, "SuperAdmin"))
                    {
                        if (Tenant == null || User.TenantId != Tenant.Id)
                        {
                            throw new ApiError("Unautorized", (int)HttpStatusCode.Unauthorized);
                        }
                    }

                    return this.GenerateToken(User, Roles, Tenant);
                }

                throw new ApiError("Incorrect Password", (int)HttpStatusCode.BadRequest);
            }

            throw new ApiError("User Not Found", (int)HttpStatusCode.NotFound);
        }

        private dynamic GenerateToken(User User, IList<string> Roles, Tenant Tenant)
        {
            // Payload for the token.
            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, User.Email),
                new Claim(ClaimTypes.NameIdentifier, User.Id.ToString()),
                new Claim("Tenant", Tenant != null ? Tenant.Id.ToString() : ""),
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

            return new { Token = JwtToken, User.Email, Roles, Tenant = Tenant?.Id, Tenant?.SocialReason };
        }
    }
}
