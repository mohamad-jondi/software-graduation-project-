using Domain.DTOs;

public class TreatmentPlanForCreation
{
    public int TreatmentPlanID { get; set; }
    public int CaseID { get; set; }
    public string PlanDescription { get; set; }
    public ICollection<DrugDTO> Drugs { get; set; }
}