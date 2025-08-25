using System.ComponentModel.DataAnnotations;

namespace Library_Management.Models
{
    public class EditBookViewModel
    {
        [Required(ErrorMessage = "Book ID is required.")]
        public Guid BookId { get; set; }
        public Guid? BookCopyId { get; set; }

        [Required(ErrorMessage = "Book title is required.")]
        [Display(Name ="Book Title asodmflasdfmladsfj")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "ISBN is required.")]
        [Display(Name ="ISBN")]
        public string? ISBN { get; set; }

        public string? Description { get; set; }


        [Required(ErrorMessage = "Genre is required.")]
        [Display(Name = "Genre")]
        public string? Genre { get; set; }


        [Display(Name = "Published Date")]
        public DateTime? PublishedDate { get; set; }

        public Guid? AuthorId { get; set; }

        [Display(Name = "Book Author")]
        public string? Author { get; set; }

        [Display(Name = "Author Profile Image URL")]
        public string? AuthorProfileImageUrl { get; set; }
        [Display(Name = "Cover Image URL")]
        public string? CoverImageUrl { get; set; }
    }
}
