using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MontyHallAPI.Repositories;

namespace MontyHallAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var audienceConfig = Configuration.GetSection("Audience");
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(audienceConfig["Secret"]));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = audienceConfig["Iss"],
                ValidateAudience = true,
                ValidAudience = audienceConfig["Aud"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,
            };

            /*services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = "TestKey";
            })
            .AddJwtBearer("TestKey", x =>
            {
                x.A
                x.RequireHttpsMetadata = false;
                x.TokenValidationParameters = tokenValidationParameters;
            });*/

            /*services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = tokenValidationParameters;
            });*/


            /*services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "AuthenticatedUserSchemeName";
            }).AddScheme<ValidTokenAuthenticationSchemeOptions, ValidTokenAuthenticationHandler>("AuthenticatedUserSchemeName", _ => { });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AuthorizedUserPolicyName", policy =>
                {
                    //policy.RequireClaim(ClaimTypes.Sid,"authToken");
                    policy.AddAuthenticationSchemes("AuthenticatedUserSchemeName");
                    policy.AddRequirements(new ValidTokenAuthorizationRequirement());
                });
                services.AddSingleton<IAuthorizationHandler, ValidTokenAuthorizationHandler>(); */

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.IncludeErrorDetails = true;
                   options.TokenValidationParameters = tokenValidationParameters;
               });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // singleton MontyHallGameRepository
            services.Add(new ServiceDescriptor(typeof(IMontyHallGameRepository), new MontyHallGameRepository()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseMvc();

        }
    }
}
