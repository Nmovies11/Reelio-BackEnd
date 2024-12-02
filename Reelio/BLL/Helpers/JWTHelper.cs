using Common.DTO;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;



namespace BLL.Helpers
{
    public class JWTHelper
    {
        private readonly string _secret;

        public JWTHelper(string secret)
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
                    new Claim(ClaimTypes.Name, body.Name),
                    new Claim(ClaimTypes.Email, body.Email)
                }),
                Expires = System.DateTime.UtcNow.AddMinutes(3),
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
                return new UserJWTDTO(
                    Guid.Parse(principal.FindFirst(ClaimTypes.NameIdentifier).Value),
                    principal.FindFirst(ClaimTypes.Name).Value,
                    principal.FindFirst(ClaimTypes.Email).Value
                );
            }
            catch (Exception)
            {
                return null; // Return null if the token validation fails
            }
        }
    }
}
