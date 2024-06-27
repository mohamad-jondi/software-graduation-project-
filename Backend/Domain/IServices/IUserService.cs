using Domain.DTOs;
using Domain.DTOs.Login;
using Domain.DTOs.Person;
namespace Domain.IServices
{
    public interface IUserService
    {
        Task<bool?> DoesUserGotPic(string username);
        Task<UserDTO> Register(RegisterModelDTO model);
        Task<PersonDTO> Login(LoginDTO login);
        Task<bool> AuthinticateEmail(AuthinticateEmailDTO randomNumber);
        Task<bool> RecovePasswordRequest(RecoverPasswordRequestDTO passwordRequest);
        Task<bool> ResetPassword (ResetPasswordDTO resetPassword);
        Task<bool> AddAdress(AddressDTO adress);
        Task<bool> CheackCredinails(RegisterModelDTO model);
    }
}
