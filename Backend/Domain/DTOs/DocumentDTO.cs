namespace Domain.DTOs
{
    public class DocumentDTO
    {
        public string Type { get; set; }
        public long DocumentId { get; set; }
        public string FileName { get; set; }
        public string FileDataBase64 { get; set; }
        public string Url { get; set; }
    }

}
