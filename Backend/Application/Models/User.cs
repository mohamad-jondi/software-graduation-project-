using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class User :BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string UserType { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public string RandomStringEmailConfirmations { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public string ProfilePicture { get; set; }
    }

}
