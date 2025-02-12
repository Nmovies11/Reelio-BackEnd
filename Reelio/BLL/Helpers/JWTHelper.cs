﻿using Common.DTO;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;



namespace BLL.Helpers
{
    public class JWTHelper
    {
        private readonly string _secret;

        public JWTHelper(string secret )
        {
            _secret = secret;
        }

        public string? GenerateToken(UserJWTDTO? body)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtTokenBytes = Encoding.UTF8.GetBytes(_secret);
            var signingcredentials = new SigningCredentials(new SymmetricSecurityKey(jwtTokenBytes), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, body.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = signingcredentials
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public UserJWTDTO? ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtTokenBytes = Encoding.UTF8.GetBytes(_secret);
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(jwtTokenBytes)
            };

            try
            {   
                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
                var claims = principal.Claims.ToList();

                return new UserJWTDTO(
                    Guid.Parse(principal.FindFirst(ClaimTypes.NameIdentifier).Value)
                );
            }
            catch (Exception)
            {
                return null; // Return null if the token validation fails
            }
        }
    }
}
