namespace Domain.DTOs.Person
{
    public class PersonDTO
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string UserType { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public double Age { get; set; }
        public double? LatestRecordedWeight { get; set; }
        public double? LatestRecordedHeight { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string Occupation { get; set; }
        public string PersonType { get; set; }
        public bool isVerifedDoctor { get; set; }
        public bool isAdmin { get; set; }
    }
}
