using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solution.Models
{
    [Table("TransactionDocuments")]
    public class TransactionDocumentModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 3)]
        public string FileName { get; set; }

        [Required]
        public long FileSize { get; set; }

        [Required]
        public string FileType { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 3)]
        public string Path { get; set; }

        public DateTime CreatedAt { get; set; } 


    }
}
