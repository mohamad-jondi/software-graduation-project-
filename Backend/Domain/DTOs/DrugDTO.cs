namespace Domain.DTOs
{
    public class DrugDTO
    {
        public int DrugID { get; set; }
        public string Name { get; set; }
        public string GenericName { get; set; }
        public string BrandName { get; set; }
        public string DosageForm { get; set; }
        public string Strength { get; set; }
        public string RouteOfAdministration { get; set; }
        public string Frequency { get; set; }
        public string Duration { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int PrescribedByID { get; set; }
        public int PatientID { get; set; }
        public string SideEffects { get; set; }
        public string Contraindications { get; set; }
        public string Interactions { get; set; }
        public string StorageInstructions { get; set; }
        public int Quantity { get; set; }
        public string RefillInfo { get; set; }
        public string Manufacturer { get; set; }
        public string BatchNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
