using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace APINetCore.Service
{
    public class JwtService
    {
        private readonly string _secretKey; // Clave secreta (debe ser segura)
        private readonly string _issuer; // Emisor del token (puede ser tu dominio)
        private readonly string _audience;
        private readonly int _expires;
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
    
            _secretKey = _configuration.GetValue<string>("JwtSettings:SecretKey");
            _issuer = _configuration.GetValue<string>("JwtSettings:IssuerToken");
            _audience = _configuration.GetValue<string>("JwtSettings:AudienceToken");
            _expires = _configuration.GetValue<int>("JwtSettings:ExpireMinute");

        }

        public string GenerateToken(string userId)
        {
            ///
          
            ///

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userId) // Puedes agregar más claims aquí
                }),
                Expires = DateTime.UtcNow.AddMinutes(_expires), // Tiempo de expiración del token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _issuer,
                Audience = _audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
