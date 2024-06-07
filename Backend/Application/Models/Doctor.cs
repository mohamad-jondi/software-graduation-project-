using Data.enums;

namespace Data.Models
{
    public class Doctor : Person 
    {
        public string Specialization { get; set; }
        public DoctorWorkType DoctorWorkType { get; set; }
        public ICollection<Credential> credential { get; set; }
        public ICollection<Callender> Callender { get; set; }
        public ICollection<Avaliability> Avalible {get; set; }
        public ICollection<Case> Cases { get; set; } 
        public ICollection<DoctorRating> DoctorRatings { get; set;}
    }
}
