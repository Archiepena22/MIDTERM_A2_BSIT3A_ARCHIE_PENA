namespace Library_Management.Models
{
    public class BookCopyDetailsViewModel
    {
        public Guid Id { get; set; }
        public string? CoverImageUrl { get; set; }
        public string? Condition { get; set; }
        public string? Source { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? PulloutDate { get; set; }
        public string? PulloutReason { get; set; }
        public bool IsPulledOut => PulloutDate.HasValue;
    }
}
