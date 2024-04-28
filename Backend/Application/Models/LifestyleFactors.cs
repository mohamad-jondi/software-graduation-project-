using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class LifestyleFactors : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LifestyleFactorsId { get; set; } 
        public bool IsSmoker { get; set; }
        public bool IsAlcoholConsumer { get; set; }
        public string ExerciseHabits { get; set; }
        public string DietaryPreferences { get; set; }
        public int PatientId { get; set; }
        public Patient? Patient { get; set; }
    }
}
