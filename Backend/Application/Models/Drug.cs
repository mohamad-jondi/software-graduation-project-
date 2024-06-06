namespace Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace Data.Models
    {
        public class Drug : BaseEntity
        {
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            [Key]
            public int DrugID { get; set; }

            [Required]
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

            [ForeignKey("DoctorID")]
            public int PrescribedByID { get; set; }

            public Doctor PrescribedBy { get; set; }

            [ForeignKey("PatientID")]
            public int PatientID { get; set; }

            public Patient Patient { get; set; }

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

}
