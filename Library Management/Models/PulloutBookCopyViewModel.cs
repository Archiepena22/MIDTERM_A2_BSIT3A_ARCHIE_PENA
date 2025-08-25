using System.ComponentModel.DataAnnotations;

namespace Library_Management.Models
{
    public class PulloutBookCopyViewModel
    {
        public Guid BookCopyId { get; set; }
        
        [Required]
        [Display(Name = "Reason for Pull-out")]
        public string? PulloutReason { get; set; }
        
        public string? BookTitle { get; set; }
        public string? CoverImageUrl { get; set; }
    }
}
