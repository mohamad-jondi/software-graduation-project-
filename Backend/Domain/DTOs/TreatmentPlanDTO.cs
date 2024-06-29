using System.Collections.Generic;

namespace Domain.DTOs
{
    public class TreatmentPlanDTO
    {
        public int TreatmentPlanID { get; set; }
        public int CaseID { get; set; }
        public string DrugUsage { get; set; }
        public ICollection<DrugDTO> Drugs { get; set; }
    }
}
