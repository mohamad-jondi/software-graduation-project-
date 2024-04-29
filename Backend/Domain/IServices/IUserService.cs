using Domain.DTOs;

namespace Domain.IServices
{
    public interface IUserService
    {
        Task Register(RegisterModelDTO model);
        Task<JWTTokensDTO> Login(LoginDTO Dto);
    }
}
