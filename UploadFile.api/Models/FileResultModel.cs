namespace UploadFile.api.Models
{

    public class FileResultModel
    {
        public string? FileName { get; set; }
        public string? Extension { get; set; }
        public long Size { get; set; }
        public string? Url { get; set; }
        public string? BlobId { get; set; }
        public bool Exito {get;set;}
        public string? Error { get; set; }
    }
}

