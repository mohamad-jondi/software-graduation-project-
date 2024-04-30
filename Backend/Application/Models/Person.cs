using Data.enums;
using Data.Models.Data.Models;

namespace Data.Models
{
    public class Person : User
    {
        public DateTime DateOfBirth { get; set; }
        public double? LatestRecordedWeight { get; set; }
        public double? LatestRecordedHeight { get; set; }
        public Gender Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string Occupation { get; set; }
        public PersonType PersonType { get; set; }
        public ICollection<Callender> CallenderAppointments { get; set; }

        public ICollection<EmergencyContactInfo> EmergencyContactInfo { get; set; }
    }

}
