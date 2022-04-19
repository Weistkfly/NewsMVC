using Microsoft.IdentityModel.Tokens;
using NewsAPI2019_2820.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NewsAPI2019_2820.Services
{
    public class TokenProvider : ITokenProvider
    {

        private readonly string _issuer;
        private readonly string _audience;
        private readonly string _secretKey;

        private string _algorithm { get; }
        private readonly SymmetricSecurityKey _signingKey;

        public TokenProvider(string issuer, string audience, string secretKey)
        {
            _issuer = issuer;
            _audience = audience;
            _secretKey = secretKey;
            _algorithm = SecurityAlgorithms.HmacSha256Signature;
            _signingKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));
        }

        public string CreateToken(User user, DateTime expirationDate)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            List<Claim> claims = new List<Claim>();
            if (user != null)
            {
                claims.Add(new Claim(ClaimTypes.Name, user.Username));
            }

            ClaimsIdentity identity = new ClaimsIdentity(claims);
            SecurityToken securityToken = tokenHandler.CreateJwtSecurityToken(new SecurityTokenDescriptor
            {
                Audience = _audience,
                Issuer = _issuer,
                SigningCredentials = new SigningCredentials(_signingKey, _algorithm),
                Expires = expirationDate.ToUniversalTime(),
                Subject = identity,
            });

            return tokenHandler.WriteToken(securityToken);
        }

        public TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters
            {
                IssuerSigningKey = _signingKey,
                ValidAudience = _audience,
                ValidIssuer = _issuer,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(0)
            };
        }


    }
}
