using System.ComponentModel.DataAnnotations;
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
        public ICollection<Documents> RelatedDocumtents { get; set; }

        public ICollection<Chat> Chats{ get; set; }
        [MaxLength(25 * 1024 * 1024)]
        public byte[] ProfilePicture { get; set; }
    }

}
