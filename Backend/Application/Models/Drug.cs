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
        [ForeignKey("CaseID")]
        public int CaseID { get; set; }
        public Case Case { get; set; }


        public int QuantityConsumed{ get; set; }

            public DrugDosageTime DrugDosageTime { get; set; }


        }
    }


