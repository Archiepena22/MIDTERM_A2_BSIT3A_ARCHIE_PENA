using Library_Management.Models;
using Library_Management_Domain.Entities;

public class BookService
{
    private readonly ICollection<Book> _books = new List<Book>();
    private readonly ICollection<Author> _authors = new List<Author>();
    private readonly ICollection<BookCopy> _bookCopies = new List<BookCopy>();

    private BookService()
    {
        SeedData();
    }

    private void SeedData()
    {
        // === First Book ===
        var author1 = new Author
        {
            Id = Guid.NewGuid(),
            Name = "George Orwell",
            Biography = "English novelist, essayist, journalist and critic.",
            BirthDate = new DateTime(1903, 6, 25),
            ProfileImageUrl = "https://example.com/orwell.jpg",
            Books = new List<Book>()
        };

        var book1 = new Book
        {
            Id = Guid.NewGuid(),
            Title = "1984",
            ISBN = "9780451524935",
            Description = "A dystopian social science fiction novel and cautionary tale.",
            Genre = "Fiction",
            PublishedDate = new DateTime(1949, 6, 8)
        };

        var bookItem1 = new BookCopy
        {
            Id = Guid.NewGuid(),
            CoverImageUrl = "https://bookcoverarchive.com/wp-content/uploads/amazon/1984.jpg",
            Condition = "Good",
            Source = "Donation",
            AddedDate = DateTime.Now.AddMonths(-2),
            Book = book1
        };

        author1.Books.Add(book1);

        // === Second Book ===
        var author2 = new Author
        {
            Id = Guid.NewGuid(),
            Name = "J.K. Rowling",
            Biography = "British author, best known for the Harry Potter series.",
            BirthDate = new DateTime(1965, 7, 31),
            ProfileImageUrl = "https://example.com/rowling.jpg",
            Books = new List<Book>()
        };

        var book2 = new Book
        {
            Id = Guid.NewGuid(),
            Title = "Harry Potter and the Philosopher's Stone",
            ISBN = "9780747532699",
            Description = "The first novel in the Harry Potter series.",
            Genre = "Fantasy",
            PublishedDate = new DateTime(1997, 6, 26)
        };

        var bookItem2 = new BookCopy
        {
            Id = Guid.NewGuid(),
            CoverImageUrl = "https://contentful.harrypotter.com/usf1vwtuqyxm/2DCs73x6P8seNobQ9zBSbO/1a5dfd6ed5fc0ed9545370470fc3d74c/English_Harry_Potter_1_Epub_9781781100219.jpg",
            Condition = "Excellent",
            Source = "Purchase",
            AddedDate = DateTime.Now.AddMonths(-6),
            Book = book2
        };

        author2.Books.Add(book2);

        // === Extra Books ===
        var extraBooks = new[]
        {
        new {
            Author = new Author
            {
                Id = Guid.NewGuid(),
                Name = "Harper Lee",
                Biography = "American novelist best known for To Kill a Mockingbird.",
                BirthDate = new DateTime(1926, 4, 28),
                ProfileImageUrl = "https://example.com/harper.jpg",
                Books = new List<Book>()
            },
            Book = new Book
            {
                Id = Guid.NewGuid(),
                Title = "To Kill a Mockingbird",
                ISBN = "9780061120084",
                Description = "A novel about racial injustice in the Deep South.",
                Genre = "Classic",
                PublishedDate = new DateTime(1960, 7, 11)
            },
            Cover = "https://images-na.ssl-images-amazon.com/images/I/81OdwZG5SSL.jpg"
        },
        new {
            Author = new Author
            {
                Id = Guid.NewGuid(),
                Name = "J.R.R. Tolkien",
                Biography = "English writer, poet, philologist, and academic.",
                BirthDate = new DateTime(1892, 1, 3),
                ProfileImageUrl = "https://example.com/tolkien.jpg",
                Books = new List<Book>()
            },
            Book = new Book
            {
                Id = Guid.NewGuid(),
                Title = "The Hobbit",
                ISBN = "9780547928227",
                Description = "A fantasy novel about Bilbo Baggins' adventure.",
                Genre = "Fantasy",
                PublishedDate = new DateTime(1937, 9, 21)
            },
            Cover = "https://m.media-amazon.com/images/I/81t2CVWEsUL.jpg"
        },
        new {
            Author = new Author
            {
                Id = Guid.NewGuid(),
                Name = "F. Scott Fitzgerald",
                Biography = "American novelist and short story writer.",
                BirthDate = new DateTime(1896, 9, 24),
                ProfileImageUrl = "https://example.com/fitzgerald.jpg",
                Books = new List<Book>()
            },
            Book = new Book
            {
                Id = Guid.NewGuid(),
                Title = "The Great Gatsby",
                ISBN = "9780743273565",
                Description = "A story of the mysterious Jay Gatsby and the American dream.",
                Genre = "Classic",
                PublishedDate = new DateTime(1925, 4, 10)
            },
            Cover = "https://m.media-amazon.com/images/I/81af+MCATTL.jpg"
        },
        new {
            Author = new Author
            {
                Id = Guid.NewGuid(),
                Name = "Suzanne Collins",
                Biography = "American television writer and author.",
                BirthDate = new DateTime(1962, 8, 10),
                ProfileImageUrl = "https://example.com/collins.jpg",
                Books = new List<Book>()
            },
            Book = new Book
            {
                Id = Guid.NewGuid(),
                Title = "The Hunger Games",
                ISBN = "9780439023481",
                Description = "A dystopian novel set in the post-apocalyptic nation of Panem.",
                Genre = "Dystopian",
                PublishedDate = new DateTime(2008, 9, 14)
            },
            Cover = "https://m.media-amazon.com/images/I/61JfGcL2ljL.jpg"
        },
        new {
            Author = new Author
            {
                Id = Guid.NewGuid(),
                Name = "Mary Shelley",
                Biography = "English novelist best known for Frankenstein.",
                BirthDate = new DateTime(1797, 8, 30),
                ProfileImageUrl = "https://example.com/shelley.jpg",
                Books = new List<Book>()
            },
            Book = new Book
            {
                Id = Guid.NewGuid(),
                Title = "Frankenstein",
                ISBN = "9780486282114",
                Description = "A gothic novel about Victor Frankenstein and his creation.",
                Genre = "Horror",
                PublishedDate = new DateTime(1818, 1, 1)
            },
            Cover = "https://m.media-amazon.com/images/I/81Fhc2wAE0L.jpg"
        }
    };

        // Add extras to collections
        _authors.Add(author1);
        _authors.Add(author2);
        _books.Add(book1);
        _books.Add(book2);
        _bookCopies.Add(bookItem1);
        _bookCopies.Add(bookItem2);

        foreach (var entry in extraBooks)
        {
            entry.Author.Books.Add(entry.Book);
            _authors.Add(entry.Author);
            _books.Add(entry.Book);
            _bookCopies.Add(new BookCopy
            {
                Id = Guid.NewGuid(),
                CoverImageUrl = entry.Cover,
                Condition = "New",
                Source = "Purchase",
                AddedDate = DateTime.Now.AddDays(-new Random().Next(1, 365)),
                Book = entry.Book
            });
        }
    }


    public void AddBook(AddBookViewModel book)
    {
        ArgumentNullException.ThrowIfNull(book, nameof(book));

        var newBook = new Book
        {
            Id = Guid.NewGuid(),
            Title = book.Title,
            ISBN = book.ISBN,
            Description = book.Description,
            Genre = book.Genre,
            PublishedDate = book.PublishedDate,
        };
        _books.Add(newBook);

        var newAuthor = new Author
        {
            Id = Guid.NewGuid(),
            Name = book.Author,
            ProfileImageUrl = book.AuthorProfileImageUrl,
            Books = new List<Book>()  // Initialize the Books list!
        };

        // Link the new book to the new author
        newAuthor.Books.Add(newBook);

        _authors.Add(newAuthor);

        var newBookItem = new BookCopy
        {
            Id = Guid.NewGuid(),
            CoverImageUrl = book.CoverImageUrl,
            Condition = book.Condition,
            Source = book.Source,
            AddedDate = DateTime.Now,
            Book = newBook
        };
        _bookCopies.Add(newBookItem);
    }



    public IEnumerable<BookListViewModel> GetBooks(bool includeArchived = false)
    {
        return _books
            .Where(b => includeArchived || !b.IsArchived)
            .Select(b => new BookListViewModel
            {
                BookId = b.Id,
                Title = b.Title,
                ISBN = b.ISBN,
                Description = b.Description,
                Genre = b.Genre,
                PublishedDate = b.PublishedDate,
                CoverImageUrl = _bookCopies.FirstOrDefault(bi => bi.Book?.Id == b.Id)?.CoverImageUrl,
                AuthorName = _authors.FirstOrDefault(a => a.Books?.Any(bk => bk.Id == b.Id) == true)?.Name,
                AuthorProfileImageUrl = _authors.FirstOrDefault(a => a.Books?.Any(bk => bk.Id == b.Id) == true)?.ProfileImageUrl,
                TotalCopies = _bookCopies.Count(bi => bi.Book?.Id == b.Id),
                AvailableCopies = _bookCopies.Count(bi => bi.Book?.Id == b.Id && bi.PulloutDate == null)
            });
    }

    public EditBookViewModel GetBookById(Guid id)
    {
        var bookViewModel = GetBooks().FirstOrDefault(b => b.BookId == id) ?? throw new KeyNotFoundException("Book not found");
        var editBookViewModel = new EditBookViewModel
        {
            BookId = bookViewModel.BookId,
            Title = bookViewModel.Title,
            ISBN = bookViewModel.ISBN,
            Description = bookViewModel.Description,
            Genre = bookViewModel.Genre,
            PublishedDate = bookViewModel.PublishedDate,
            AuthorId = _authors.FirstOrDefault(a => a.Name == bookViewModel.AuthorName)?.Id,
            Author = bookViewModel.AuthorName,
            AuthorProfileImageUrl = bookViewModel.AuthorProfileImageUrl,
            CoverImageUrl = bookViewModel.CoverImageUrl,
        };

        return editBookViewModel ?? throw new KeyNotFoundException("Book not found");
    }

    internal void UpdateBook(EditBookViewModel vm)
    {
        ArgumentNullException.ThrowIfNull(vm, nameof(vm));

        // Find the book by ID
        var book = _books.FirstOrDefault(b => b.Id == vm.BookId) ?? throw new KeyNotFoundException("Book not found");

        // Update basic book properties
        book.Title = vm.Title;
        book.ISBN = vm.ISBN;
        book.Description = vm.Description;
        book.Genre = vm.Genre;
        book.PublishedDate = vm.PublishedDate;

        // Find the old author (if any) so we can remove the book if author changes
        var oldAuthor = _authors.FirstOrDefault(a => a.Books?.Any(bk => bk.Id == book.Id) == true);

            // Find or create the new author
        Author? author = null;
        if (vm.AuthorId.HasValue)
        {
            author = _authors.FirstOrDefault(a => a.Id == vm.AuthorId.Value);
        }
        if (author == null)
        {
            author = new Author
            {
                Id = vm.AuthorId ?? Guid.NewGuid(),
                Name = vm.Author,
                ProfileImageUrl = vm.AuthorProfileImageUrl,
                Books = new List<Book>()
            };
            _authors.Add(author);
        }
        else
        {
            // Update existing author info
            author.Name = vm.Author;
            author.ProfileImageUrl = vm.AuthorProfileImageUrl;
        }

        // If author changed, remove the book from old author
        if (oldAuthor != null && oldAuthor.Id != author.Id)
        {
            oldAuthor.Books.Remove(book);
        }

        // Ensure the book is linked to the new author
        if (!author.Books.Any(bk => bk.Id == book.Id))
        {
            author.Books.Add(book);
        }

        // Update or create the BookCopy (cover image)
        var bookCopy = _bookCopies.FirstOrDefault(bi => bi.Book?.Id == vm.BookId);
        if (bookCopy != null)
        {
            bookCopy.CoverImageUrl = vm.CoverImageUrl;
        }
        else
        {
            bookCopy = new BookCopy
            {
                Id = Guid.NewGuid(),
                CoverImageUrl = vm.CoverImageUrl,
                AddedDate = DateTime.Now,
                Book = book
            };
            _bookCopies.Add(bookCopy);
        }
    }


    public void ArchiveBook(Guid id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id) ?? throw new KeyNotFoundException("Book not found");
        book.IsArchived = true;
    }

    public void RestoreBook(Guid id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id) ?? throw new KeyNotFoundException("Book not found");
        book.IsArchived = false;
    }

    public void DeleteBook(Guid id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id) ?? throw new KeyNotFoundException("Book not found");
        _books.Remove(book);
        var author = _authors.FirstOrDefault(a => a.Books?.Any(bk => bk.Id == id) == true);
        if (author != null)
        {
            author.Books.Remove(book);
            if (!author.Books.Any())
            {
                _authors.Remove(author);
            }
        }
        var bookCopies = _bookCopies.Where(bi => bi.Book?.Id == id).ToList();
        foreach (var bookCopy in bookCopies)
        {
            _bookCopies.Remove(bookCopy);
        }
    }

    // Singleton pattern
    private static BookService? _instance;
    public static BookService Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BookService();
            }
            return _instance;
        }
    }

    public void AddCopy(Guid bookId)
    {
        var book = _books.FirstOrDefault(b => b.Id == bookId);
        if (book == null)
            throw new KeyNotFoundException("Book not found.");

        var coverImage = _bookCopies.FirstOrDefault(bc => bc.Book?.Id == bookId)?.CoverImageUrl;

        var newCopy = new BookCopy
        {
            Id = Guid.NewGuid(),
            Book = book,
            AddedDate = DateTime.Now,
            Condition = "New",
            Source = "Manual Add",
            CoverImageUrl = coverImage
        };

        _bookCopies.Add(newCopy);
    }

    // Pull-out functionality
    public void PulloutBookCopy(Guid bookCopyId, string reason)
    {
        var bookCopy = _bookCopies.FirstOrDefault(bc => bc.Id == bookCopyId);
        if (bookCopy == null)
            throw new KeyNotFoundException("Book copy not found.");

        if (bookCopy.PulloutDate.HasValue)
            throw new InvalidOperationException("This book copy is already pulled out.");

        bookCopy.PulloutDate = DateTime.Now;
        bookCopy.PulloutReason = reason;
    }

    public Library_Management.Models.BookDetailsViewModel? GetBookDetails(Guid id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);
        if (book == null)
            return null;

        var author = _authors.FirstOrDefault(a => a.Books?.Any(bk => bk.Id == id) == true);
        var bookCopies = _bookCopies.Where(bc => bc.Book?.Id == id)
            .Select(bc => new Library_Management.Models.BookCopyDetailsViewModel
            {
                Id = bc.Id,
                CoverImageUrl = bc.CoverImageUrl,
                Condition = bc.Condition,
                Source = bc.Source,
                AddedDate = bc.AddedDate,
                PulloutDate = bc.PulloutDate,
                PulloutReason = bc.PulloutReason
            }).ToList();

        return new Library_Management.Models.BookDetailsViewModel
        {
            BookId = book.Id,
            Title = book.Title,
            ISBN = book.ISBN,
            Description = book.Description,
            Genre = book.Genre,
            PublishedDate = book.PublishedDate,
            AuthorName = author?.Name,
            AuthorId = author?.Id,
            AuthorProfileImageUrl = author?.ProfileImageUrl,
            IsArchived = book.IsArchived,
            BookCopies = bookCopies
        };
    }

    // Internal accessors for AuthorService to maintain data consistency
    internal ICollection<Author> GetAuthorsInternal() => _authors;
    internal ICollection<Book> GetBooksInternal() => _books;
    internal ICollection<BookCopy> GetBookCopiesInternal() => _bookCopies;

}
