using Data.Models;
using Domain.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Data.Interfaces;

namespace Domain.Services
{
    public class JWTTokentService : IJWTTokenServices
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _context;

        public JWTTokentService(IConfiguration configuration, IUnitOfWork context)
        {
            _configuration = configuration;
            _context = context;
        }
        public JWTTokens Authenticate(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            var refreshToken = Guid.NewGuid();
            var x = new JWTTokensRefresh { RefreshToken = refreshToken, UserID = user.Id };
            _context.GetRepositories<JWTTokensRefresh>().Add(x);


            return new JWTTokens
            {
                Token = tokenString,
                refToken = refreshToken.ToString(),
            };
        }

    }
}
