using Data.enums;

namespace Domain.DTOs.Login
{
    public class RegisterModelDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public UserType UserType { get; set; }

    }
}
