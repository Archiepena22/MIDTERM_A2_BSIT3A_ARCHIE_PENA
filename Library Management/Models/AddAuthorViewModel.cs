using System.ComponentModel.DataAnnotations;

namespace Library_Management.Models
{
    public class AddAuthorViewModel
    {
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
