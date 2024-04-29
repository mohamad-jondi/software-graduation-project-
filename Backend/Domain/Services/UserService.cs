using Domain.DTOs;
using Domain.IServices;

namespace Domain.Services
{
    public class UserService : IUserService
    {
        public Task<JWTTokensDTO> Login(LoginDTO Dto)
        {
            throw new NotImplementedException();
        }

        public Task Register(RegisterModelDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
