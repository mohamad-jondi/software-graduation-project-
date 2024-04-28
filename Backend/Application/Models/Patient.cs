using Data.enums;
using Data.Models.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Patient : Person
    {
        public string Occupation { get; set; }

        [ForeignKey("EmergencyContactInfoID")]
        public int EmergancyContactID { get; set; }
        public EmergencyContactInfo? EmergancyContact { get; set; }

        public ICollection<Operation> operations { get; set; }
        public ICollection<Allergy> Allergies { get; set; }
        public ICollection<Vaccination> Vaccinations { get; set; }

        public ICollection<Doctor> Doctors { get; set; }

    }
}
