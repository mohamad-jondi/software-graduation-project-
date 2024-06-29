using Data.Models;

namespace Domain.IServices
{
    public interface IJWTTokenServices
    {
        Task<JWTTokens> Authenticate(User user); 
        Task<JWTTokens> AuthenticateUsingRefreshTokenAsync(Guid RefreshToken);
    }
}
