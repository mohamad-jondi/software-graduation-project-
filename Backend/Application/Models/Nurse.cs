namespace Data.Models
{
    public class Nurse : Person
    {
        ICollection<Credential> Credentials { get; set; }

        ICollection<Case> cases { get; set; }


    }
}
