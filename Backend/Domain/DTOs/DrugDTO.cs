namespace Domain.DTOs
{
    public class DrugDTO
    {
        public int DrugID { get; set; }
        public string Name { get; set; }
        public string Strength { get; set; }
        public string Duration { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
