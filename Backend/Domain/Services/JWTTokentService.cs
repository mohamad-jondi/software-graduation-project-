using Data.Models;
using Domain.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

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
        public async Task<JWTTokens> Authenticate(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWTToken:Key"]);
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
            var ExistingRefreshToken = await _context.GetRepositories<JWTTokensRefresh>().Get().Where(x=> x.UserID ==  user.Id).FirstOrDefaultAsync();
            if (ExistingRefreshToken == null) await _context.GetRepositories<JWTTokensRefresh>().Add(x);

            else
            {
                ExistingRefreshToken.RefreshToken = refreshToken;
                await _context.GetRepositories<JWTTokensRefresh>().Update(ExistingRefreshToken);
            }


            return new JWTTokens
            {
                Token = tokenString,
                refToken = refreshToken.ToString(),
            };
        }

        public Task<JWTTokens> AuthenticateUsingRefreshTokenAsync(Guid RefreshToken)
        {
           var user = _context.GetRepositories<JWTTokensRefresh>().Get().Include(x=> x.user).Where(x=> x.RefreshToken ==  RefreshToken).FirstOrDefault().user;
            if (user != null) { return Authenticate(user); }
            else return null;
        }
    }
}
