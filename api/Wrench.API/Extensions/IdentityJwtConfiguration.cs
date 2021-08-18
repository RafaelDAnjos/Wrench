using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Wrench.Data.Context;
using Wrench.Domain.Entities.Identity;

namespace Wrench.API.Extensions
{
    public static class IdentityJwtConfiguration
    {
        public static IServiceCollection AddIdentityJwt(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>()
                    .AddRoles<AppRole>()
                    .AddEntityFrameworkStores<WrenchDbContext>();

            //JWT
            var key = Encoding.ASCII.GetBytes("DISCIPLINALES20212X");
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt =>
            {
                jwt.RequireHttpsMetadata = false;
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }
    }
}