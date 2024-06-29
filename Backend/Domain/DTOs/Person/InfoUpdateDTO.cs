using Data.enums;

namespace Domain.DTOs.Person
{
    public class InfoUpdateDTO
    {
        public string username { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public double? LatestRecordedWeight { get; set; }
        public double? LatestRecordedHeight { get; set; }
        public Gender Gender { get; set; }
        public string? MaritalStatus { get; set; }
        public string? Occupation { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }
}
