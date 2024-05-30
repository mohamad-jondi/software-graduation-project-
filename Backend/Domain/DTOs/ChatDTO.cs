namespace Domain.DTOs
{
    public class ChatForShowingFromTheOutsideDTO
    {
        public string PatientUsername { get; set; }
        public string DoctorUsername { get; set; }
        public bool IsTheLastSenderMe { get; set; }
        public string lastSentMassagess{ get; set; }
        public int numberOfMessages { get; set; }
    }
}
