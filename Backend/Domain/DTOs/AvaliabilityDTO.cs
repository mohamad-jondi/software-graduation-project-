namespace Domain.DTOs
{
    public class AvaliabilityDTO
    {
        public TimeOnly AvaliabilityTimeStart { get; set; }
        public TimeOnly AvaliabilityTimeEnd { get; set; }
        public DateOnly AvaliabilityDateStart { get; set; }
        public DateOnly AvaliabilityDateEnd { get; set; }
    }
}
