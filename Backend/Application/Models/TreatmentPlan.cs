using Data.Models;
using Data.Models.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

public class TreatmentPlan : BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TreatmentPlanID { get; set; }

    [ForeignKey("CaseID")]
    public int CaseID { get; set; }
    public Case Case { get; set; }
    public string DrugUsage { get; set; }
    public ICollection<Drug> Drugs { get; set; }
}