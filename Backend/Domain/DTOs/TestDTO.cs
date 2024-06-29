using Data.enums;
using System;

namespace Domain.DTOs
{
    public class TestDTO
    {
        public int TestID { get; set; }
        public string TestName { get; set; }
        public string TestDescription { get; set; }
        public int DoctorID { get; set; }
        public int PatientID { get; set; }
        public TestStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompletionDate { get; set; }
    }
}
