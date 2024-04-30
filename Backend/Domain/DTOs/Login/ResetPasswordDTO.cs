namespace Domain.DTOs.Login
{
    public class ResetPasswordDTO
    {
        public string Username { get; set; }
        public string RandomNumber { get; set; }
        public string Password { get; set; }
    }
}
