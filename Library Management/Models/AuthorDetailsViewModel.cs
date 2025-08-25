namespace Library_Management.Models
{
    public class AuthorDetailsViewModel
    {
        public Guid AuthorId { get; set; }
        public string? Name { get; set; }
        public string? Biography { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? ProfileImageUrl { get; set; }
        public bool IsArchived { get; set; }
        public List<BookListViewModel> Books { get; set; } = new List<BookListViewModel>();
    }
}
