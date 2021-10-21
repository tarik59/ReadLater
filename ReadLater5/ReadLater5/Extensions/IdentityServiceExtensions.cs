using Data;
using Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadLater5.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services,IConfiguration config)
        {
            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

            });

            
            services.AddAuthentication()
             .AddOpenIdConnect("oidc", options =>
             {
                 options.Authority = "https://accounts.google.com";

                 options.ClientId = config["GoogleAuth:ClientID"];
                 options.ClientSecret = config["GoogleAuth:ClientSecret"];
                 options.ResponseType = OpenIdConnectResponseType.Code;
             })
              .AddOpenIdConnect("oidc2", options =>
              {
                  options.Authority = "https://www.facebook.com";

                  options.ClientId = config["FacebookAuth:ClientID"];
                  options.ClientSecret = config["FacebookAuth:ClientSecret"];
                  options.ResponseType = OpenIdConnectResponseType.Code;
              });
            return services;
        }
    }
}
