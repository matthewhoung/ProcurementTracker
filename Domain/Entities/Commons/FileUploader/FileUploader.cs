namespace Domain.Entities.Commons.FileUploader
{
    public class FileUploader
    {
        public int FileId { get; set; }
        public int FormId { get; set; }
        public int UploaderId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
