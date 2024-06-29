using Data.enums;

namespace Domain.DTOs
{
    public class DrugDTO
    {
        public string Name { get; set; }
        public string Duration { get; set; }
        public int QuantityConsumed { get; set; }
        public DrugDosageTime DrugDosageTime { get; set; }
    }
}
