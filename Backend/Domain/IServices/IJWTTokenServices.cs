using Data.Models;

namespace Domain.IServices
{
    public interface IJWTTokenServices
    {
        JWTTokens Authenticate(User user); 
    }
}
