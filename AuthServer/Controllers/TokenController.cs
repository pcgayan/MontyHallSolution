using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace AuthServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        private readonly IOptions<Audience> settings;

        private readonly ILogger<TokenController> logger;

        public TokenController(IOptions<Audience> settings, ILogger<TokenController> logger)
        {
            this.settings = settings;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult Get(string personalId)
        {
            if (personalId == null)
            {
                return Unauthorized();
            }

            try
            {
                var now = DateTime.UtcNow;

                var claims = new Claim[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, personalId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64),
                new Claim("personalId", personalId)
                };

                var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(settings.Value.Secret));
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingKey,
                    ValidateIssuer = true,
                    ValidIssuer = settings.Value.Iss,
                    ValidateAudience = true,
                    ValidAudience = settings.Value.Aud,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    RequireExpirationTime = true,

                };

                TimeSpan expiryTime = TimeSpan.FromMinutes(15);
                var jwt = new JwtSecurityToken(
                    issuer: settings.Value.Iss,
                    audience: settings.Value.Aud,
                    claims: claims,
                    notBefore: now,
                    expires: now.Add(expiryTime),
                    signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                );
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                var responseJson = new
                {
                    access_token = encodedJwt,
                    expires_in = (int)expiryTime.TotalSeconds
                };

                logger.LogDebug("Token generated for user " + personalId + encodedJwt);
                return Json(responseJson);
            } catch (Exception e)
            {
                logger.LogError(e.Message,e);
                return Unauthorized();
            }

        }
    }

    public class Audience
    {
        public string Secret { get; set; }
        public string Iss { get; set; }
        public string Aud { get; set; }
    }

}
