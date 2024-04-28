using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Address
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressID { get; set; } 
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        [ForeignKey("UserID")]
        public int UserId { get; set; }

        public User? user { get; set; }
    }
}