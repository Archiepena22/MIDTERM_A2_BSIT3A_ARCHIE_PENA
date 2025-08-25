namespace Library_Management.Models
{
    public class BookDetailsViewModel
    {
        public Guid BookId { get; set; }
        public string? Title { get; set; }
        public string? ISBN { get; set; }
        public string? Description { get; set; }
        public string? Genre { get; set; }
        public DateTime? PublishedDate { get; set; }
        public string? AuthorName { get; set; }
        public Guid? AuthorId { get; set; }
        public string? AuthorProfileImageUrl { get; set; }
        public bool IsArchived { get; set; }
        public List<BookCopyDetailsViewModel> BookCopies { get; set; } = new List<BookCopyDetailsViewModel>();
        public int TotalCopies => BookCopies.Count;
        public int AvailableCopies => BookCopies.Count(bc => !bc.IsPulledOut);
        public int PulledOutCopies => BookCopies.Count(bc => bc.IsPulledOut);
    }
}
