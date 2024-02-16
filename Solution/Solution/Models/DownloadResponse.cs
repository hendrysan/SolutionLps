namespace Solution.Models
{
    public class DownloadResponse : DefaultResponse
    {
        public byte[] Document { get; set; }
        public TransactionDocumentModel DocumentInfo { get; set; }
    }
}
