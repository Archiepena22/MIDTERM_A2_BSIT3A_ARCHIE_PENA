namespace Library_Management.Models
{
    public class BookListViewModel
    {
        public Guid BookId { get; set; }
        public string? Title { get; set; } = default!;
        public string? ISBN { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public string? Genre { get; set; } = default!;
        public DateTime? PublishedDate { get; set; } = default!;
        public string? CoverImageUrl { get; set; } = default!;

        public string? AuthorName { get; set; } = default!;
        public string? AuthorProfileImageUrl { get; set; } = default!;
        public int TotalCopies { get; set; } = 0;
        public int AvailableCopies { get; set; } = 0;
    }
}
