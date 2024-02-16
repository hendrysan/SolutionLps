namespace Solution.Models
{
    public class DocumentListResponse : DefaultResponse
    {
        public List<TransactionDocumentModel> Documents { get; set; }
    }
}
