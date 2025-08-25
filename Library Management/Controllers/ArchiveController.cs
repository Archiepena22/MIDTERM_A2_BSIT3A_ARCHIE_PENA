using Library_Management.Models;
using Library_Management.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management.Controllers
{
    public class ArchiveController : Controller
    {
        public IActionResult Index()
        {
            var archivedBooks = BookService.Instance.GetBooks(includeArchived: true)
                                                   .Where(b => BookService.Instance.GetBooksInternal()
                                                              .FirstOrDefault(book => book.Id == b.BookId)?.IsArchived == true)
                                                   .ToList();

            var archivedAuthors = AuthorService.Instance.GetAuthors(includeArchived: true)
                                                        .Where(a => a.IsArchived)
                                                        .ToList();

            var viewModel = new ArchiveViewModel
            {
                ArchivedBooks = archivedBooks,
                ArchivedAuthors = archivedAuthors,
                TotalArchivedBooks = archivedBooks.Count(),
                TotalArchivedAuthors = archivedAuthors.Count()
            };

            return View(viewModel);
        }
    }

    // Archive ViewModel
    public class ArchiveViewModel
    {
        public IEnumerable<BookListViewModel> ArchivedBooks { get; set; } = new List<BookListViewModel>();
        public IEnumerable<AuthorListViewModel> ArchivedAuthors { get; set; } = new List<AuthorListViewModel>();
        public int TotalArchivedBooks { get; set; }
        public int TotalArchivedAuthors { get; set; }
    }
}
