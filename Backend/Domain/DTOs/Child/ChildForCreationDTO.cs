using Data.enums;

namespace Domain.DTOs.Child
{
    public class ChildForCreationDTO
    {
        public string? Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public double? LatestRecordedWeight { get; set; }
        public double? LatestRecordedHeight { get; set; }
        public Gender? Gender { get; set; }
    }
}
