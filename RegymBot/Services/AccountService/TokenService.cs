using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PasswordHashing;
using RegymBot.AppSettings;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RegymBot.AccountService
{
    public class TokenService : ITokenService
    {
        private readonly JWTSettings _appSettings;

        public TokenService(IOptions<JWTSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public string GetHashString(string password)
        {
            return PasswordHasher.Hash(password);
        }

        public string Authenticate(Guid guid, string email, string role = "")
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Sid, guid.ToString()),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, role)
                }),                
                Audience = _appSettings.Audience,
                Issuer = _appSettings.Issuer,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}