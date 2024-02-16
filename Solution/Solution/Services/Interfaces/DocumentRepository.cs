using Microsoft.EntityFrameworkCore;
using Solution.Contexts;
using Solution.Helpers;
using Solution.Models;
using Solution.Services.Repositories;
using System.Net;

namespace Solution.Services.Interfaces
{
    public class DocumentRepository(DatabaseContext context) : IDocumentRepository
    {
        private readonly DatabaseContext _context = context;

        public async Task<DefaultResponse> Delete(Guid documentId)
        {
            DefaultResponse response = new DefaultResponse();

            try
            {
                var document = _context.TransactionDocuments.Find(documentId);
                if (document == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Document not found";
                    return response;
                }

                var delete = await MinioHelper.RemoveAsync(document.Path);

                _context.TransactionDocuments.Remove(document);
                _context.SaveChanges();



                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Document deleted successfully";
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<DownloadResponse> GetDocuments(Guid documentId)
        {
            DownloadResponse download = new DownloadResponse();
            try
            {
                var document = _context.TransactionDocuments.Find(documentId);
                if (document == null)
                {
                    return null;
                }



                var url = MinioHelper.GetStaticUrlAsync(document.Path);

                var client = new HttpClient();
                var response = await client.GetAsync(url);
                var bytes = await response.Content.ReadAsByteArrayAsync();
                var result = new byte[bytes.Length];
                bytes.CopyTo(result, 0);


                download.DocumentInfo = document;

                download.Document = result;
                return download;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<DocumentListResponse> List(Guid userId)
        {
            DocumentListResponse response = new DocumentListResponse();
            try
            {
                var documents = await _context.TransactionDocuments
                    .Where(i => i.UserId == userId)
                    .Select(i => new TransactionDocumentModel
                    {
                        Id = i.Id,
                        FileName = i.FileName,
                        FileSize = i.FileSize,
                        FileType = i.FileType,
                        Path = MinioHelper.GetStaticUrlAsync(i.Path),
                        CreatedAt = i.CreatedAt
                    })
                    .ToListAsync();

                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Documents retrieved successfully";
                response.Documents = documents;
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<DefaultResponse> Upload(IFormFile file, Guid userId)
        {
            DefaultResponse response = new DefaultResponse();
            try
            {
                if (file.Length > (10485760 * 20))
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "File size should not exceed 200MB";
                    return response;
                }

                var user = await _context.MasterUsers.FindAsync(userId);
                if (user == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "User not found";
                    return response;
                }

                string path = $"{userId.ToString()}/{file.FileName}";

                var status = await MinioHelper.SendAsync(path, file.OpenReadStream(), file.ContentType);

                if (!status)
                {
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Document upload failed";
                    return response;
                }

                var document = new TransactionDocumentModel
                {
                    UserId = userId,
                    FileName = file.FileName,
                    FileSize = file.Length,
                    FileType = file.ContentType,
                    Path = path,
                    CreatedAt = DateTime.Now
                };

                _context.TransactionDocuments.Add(document);
                await _context.SaveChangesAsync();

                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Document uploaded successfully";
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
