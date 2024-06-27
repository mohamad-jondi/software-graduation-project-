    using Data.enums;
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
            public string Duration { get; set; }

            [ForeignKey("DoctorID")]
            public int PrescribedByID { get; set; }

            public Doctor PrescribedBy { get; set; }

            [ForeignKey("PatientID")]
            public int PatientID { get; set; }

            public Patient Patient { get; set; }
            public int QuantityConsumed{ get; set; }

            public DrugDosageTime DrugDosageTime { get; set; }


        }
    }


