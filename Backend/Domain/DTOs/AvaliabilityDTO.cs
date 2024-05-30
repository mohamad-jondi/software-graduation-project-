namespace Domain.DTOs
{
    public class AvaliabilityDTO
    {
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartHour { get; set; }
        public TimeSpan EndHour { get; set; }
    }
}
