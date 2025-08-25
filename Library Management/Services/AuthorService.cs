using Library_Management.Models;
using Library_Management_Domain.Entities;

namespace Library_Management.Services
{
    public class AuthorService
    {
        // Use the same collections from BookService to maintain data consistency
        private readonly ICollection<Author> _authors;
        private readonly ICollection<Book> _books;
        private readonly ICollection<BookCopy> _bookCopies;

        private AuthorService()
        {
            // Get references to the same collections used in BookService
            _authors = BookService.Instance.GetAuthorsInternal();
            _books = BookService.Instance.GetBooksInternal();
            _bookCopies = BookService.Instance.GetBookCopiesInternal();
        }

        public IEnumerable<AuthorListViewModel> GetAuthors(bool includeArchived = false)
        {
            return _authors
                .Where(a => includeArchived || !a.IsArchived)
                .Select(a => new AuthorListViewModel
                {
                    AuthorId = a.Id,
                    Name = a.Name,
                    Biography = a.Biography,
                    BirthDate = a.BirthDate,
                    ProfileImageUrl = a.ProfileImageUrl,
                    BookCount = a.Books?.Count(b => !b.IsArchived) ?? 0,
                    IsArchived = a.IsArchived
                })
                .OrderBy(a => a.Name);
        }

        public AuthorDetailsViewModel? GetAuthorById(Guid id)
        {
            var author = _authors.FirstOrDefault(a => a.Id == id);
            if (author == null)
                return null;

            var authorBooks = author.Books?
                .Where(b => !b.IsArchived)
                .Select(b => new BookListViewModel
                {
                    BookId = b.Id,
                    Title = b.Title,
                    ISBN = b.ISBN,
                    Description = b.Description,
                    Genre = b.Genre,
                    PublishedDate = b.PublishedDate,
                    CoverImageUrl = _bookCopies.FirstOrDefault(bc => bc.Book?.Id == b.Id)?.CoverImageUrl,
                    AuthorName = author.Name,
                    AuthorProfileImageUrl = author.ProfileImageUrl,
                    TotalCopies = _bookCopies.Count(bc => bc.Book?.Id == b.Id),
                    AvailableCopies = _bookCopies.Count(bc => bc.Book?.Id == b.Id && bc.PulloutDate == null)
                }).ToList() ?? new List<BookListViewModel>();

            return new AuthorDetailsViewModel
            {
                AuthorId = author.Id,
                Name = author.Name,
                Biography = author.Biography,
                BirthDate = author.BirthDate,
                ProfileImageUrl = author.ProfileImageUrl,
                IsArchived = author.IsArchived,
                Books = authorBooks
            };
        }

        public EditAuthorViewModel? GetAuthorForEdit(Guid id)
        {
            var author = _authors.FirstOrDefault(a => a.Id == id);
            if (author == null)
                return null;

            return new EditAuthorViewModel
            {
                AuthorId = author.Id,
                Name = author.Name,
                Biography = author.Biography,
                BirthDate = author.BirthDate,
                ProfileImageUrl = author.ProfileImageUrl
            };
        }

        public void AddAuthor(AddAuthorViewModel viewModel)
        {
            ArgumentNullException.ThrowIfNull(viewModel, nameof(viewModel));

            var author = new Author
            {
                Id = Guid.NewGuid(),
                Name = viewModel.Name,
                Biography = viewModel.Biography,
                BirthDate = viewModel.BirthDate,
                ProfileImageUrl = viewModel.ProfileImageUrl,
                IsArchived = false,
                Books = new List<Book>()
            };

            _authors.Add(author);
        }

        public void UpdateAuthor(EditAuthorViewModel viewModel)
        {
            ArgumentNullException.ThrowIfNull(viewModel, nameof(viewModel));

            var author = _authors.FirstOrDefault(a => a.Id == viewModel.AuthorId);
            if (author == null)
                throw new KeyNotFoundException("Author not found");

            author.Name = viewModel.Name;
            author.Biography = viewModel.Biography;
            author.BirthDate = viewModel.BirthDate;
            author.ProfileImageUrl = viewModel.ProfileImageUrl;
        }

        public void ArchiveAuthor(Guid id)
        {
            var author = _authors.FirstOrDefault(a => a.Id == id);
            if (author == null)
                throw new KeyNotFoundException("Author not found");

            author.IsArchived = true;
            
            // Also archive all books by this author
            if (author.Books != null)
            {
                foreach (var book in author.Books)
                {
                    book.IsArchived = true;
                }
            }
        }

        public void RestoreAuthor(Guid id)
        {
            var author = _authors.FirstOrDefault(a => a.Id == id);
            if (author == null)
                throw new KeyNotFoundException("Author not found");

            author.IsArchived = false;
            
            // Optionally restore books (you might want to make this selective)
            if (author.Books != null)
            {
                foreach (var book in author.Books)
                {
                    book.IsArchived = false;
                }
            }
        }

        public void DeleteAuthor(Guid id)
        {
            var author = _authors.FirstOrDefault(a => a.Id == id);
            if (author == null)
                throw new KeyNotFoundException("Author not found");

            // Remove all book copies first
            if (author.Books != null)
            {
                foreach (var book in author.Books.ToList())
                {
                    var bookCopies = _bookCopies.Where(bc => bc.Book?.Id == book.Id).ToList();
                    foreach (var copy in bookCopies)
                    {
                        _bookCopies.Remove(copy);
                    }
                    _books.Remove(book);
                }
            }

            _authors.Remove(author);
        }

        public bool CanDeleteAuthor(Guid id)
        {
            var author = _authors.FirstOrDefault(a => a.Id == id);
            if (author == null)
                return false;

            // Check if author has any books
            return author.Books?.Any() != true;
        }

        // Singleton pattern
        private static AuthorService? _instance;
        public static AuthorService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AuthorService();
                }
                return _instance;
            }
        }
    }
}
