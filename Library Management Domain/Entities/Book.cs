using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_Domain.Entities
{
    //https://bookcoverarchive.com/
    public class Book
    {
        public Guid Id { get; set; }
        public string? Title { get; set; } = default!;
        public string? ISBN { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public string? Genre { get;set; } = default!;
        public DateTime? PublishedDate { get; set; } = default!;
        public bool IsArchived { get; set; } = false;

    }

    public class BookCopy
    {
        public Guid Id { get; set; }
        public string? CoverImageUrl { get; set; } = default!;
        public string? Condition { get; set; } = default!;
        public string? Source { get; set; } = default!;
        public DateTime? AddedDate { get; set; } = default!;
        public DateTime? PulloutDate { get; set; } = default!;
        public string? PulloutReason { get; set; } = default!;

        public Book? Book { get; set; } = default!;
    }



    public class Author
    {
        public Guid Id { get; set; }
        public string? Name { get; set; } = default!;
        public string? Biography { get; set; } = default!;
        public DateTime? BirthDate { get; set; } = default!;
        public string? ProfileImageUrl { get; set; } = default!;
        public bool IsArchived { get; set; } = false;

        public List<Book> Books { get; set; } = [];
    }
}
