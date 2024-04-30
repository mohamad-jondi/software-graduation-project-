using Domain.DTOs;
using Domain.DTOs.Login;
namespace Domain.IServices
{
    public interface IUserService
    {
        Task<bool> Register(RegisterModelDTO model);
        Task<JWTTokensDTO> Login(LoginDTO login);
        Task<bool> AuthinticateEmail(AuthinticateEmailDTO randomNumber);
        Task<bool> RecovePasswordRequest(RecoverPasswordRequestDTO passwordRequest);
        Task<bool> ResetPassword (ResetPasswordDTO resetPassword);
        Task<bool> AddAdress(AddressDTO adress);
        Task<bool> CheackCredinails(RegisterModelDTO model);
    }
}
