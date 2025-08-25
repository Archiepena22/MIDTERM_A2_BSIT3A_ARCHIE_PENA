namespace Library_Management.Models
{
    public class AuthorListViewModel
    {
        public Guid AuthorId { get; set; }
        public string? Name { get; set; }
        public string? Biography { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? ProfileImageUrl { get; set; }
        public int BookCount { get; set; }
        public bool IsArchived { get; set; }
    }
}
