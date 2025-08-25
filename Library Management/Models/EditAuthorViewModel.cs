using System.ComponentModel.DataAnnotations;

namespace Library_Management.Models
{
    public class EditAuthorViewModel
    {
        public Guid AuthorId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string? Name { get; set; }

        [Display(Name = "Biography")]
        public string? Biography { get; set; }

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Profile Image URL")]
        [DataType(DataType.Url)]
        public string? ProfileImageUrl { get; set; }
    }
}
