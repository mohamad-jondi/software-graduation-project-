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

        public bool isVerifedDoctor { get; set; }
        public bool IsAdmin { get; set; }
        public ICollection<Appointment> CallenderAppointments { get; set; }

        public ICollection<EmergencyContactInfo> EmergencyContactInfo { get; set; }
    }

}
