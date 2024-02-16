using Solution.Models;

namespace Solution.Services.Repositories
{
    public interface IDocumentRepository
    {
        Task<DefaultResponse> Upload(IFormFile file, Guid userId);
        Task<DocumentListResponse> List(Guid userId);
        Task<DefaultResponse> Delete(Guid documentId);
        Task<DownloadResponse> GetDocuments(Guid documentId);
    }
}
