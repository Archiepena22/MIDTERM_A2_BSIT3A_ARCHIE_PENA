using Library_Management.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            var books = BookService.Instance.GetBooks();
            return View(books);
        }

        public IActionResult AddModal()
        {
            return PartialView("_AddBookPartial");
        }

        [HttpPost]
        public IActionResult Add(AddBookViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BookService.Instance.AddBook(vm);
            return Ok();
        }

        public IActionResult EditModal(Guid id)
        {
            var editBookViewModel = BookService.Instance.GetBookById(id);
            if (editBookViewModel == null)
                return NotFound();

            return PartialView("_EditBookPartial", editBookViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditBookViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BookService.Instance.UpdateBook(vm);
            return Ok();
        }

        // ✅ UPDATED DeleteModal and Delete
        public IActionResult DeleteModal(Guid id)
        {
            var book = BookService.Instance.GetBookById(id);
            if (book == null)
                return NotFound();

            return PartialView("_DeleteBookPartial", book); // ✅ updated partial name
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            var book = BookService.Instance.GetBookById(id);
            if (book == null)
                return NotFound();

            BookService.Instance.DeleteBook(id);
            return Ok(); // You can return a redirect if not using AJAX
        }

    public IActionResult Details(Guid id)
    {
        var book = BookService.Instance.GetBookDetails(id);
        if (book == null)
            return NotFound();

        return View(book);
    }

        [HttpPost]
        public IActionResult AddCopy(Guid id)
        {
            try
            {
                BookService.Instance.AddCopy(id);
                return RedirectToAction("Details", new { id });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Archive(Guid id)
        {
            try
            {
                BookService.Instance.ArchiveBook(id);
                TempData["SuccessMessage"] = "Book archived successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while archiving the book: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public IActionResult Restore(Guid id)
        {
            try
            {
                BookService.Instance.RestoreBook(id);
                TempData["SuccessMessage"] = "Book restored successfully!";
                return RedirectToAction(nameof(Archive));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while restoring the book: " + ex.Message;
                return RedirectToAction(nameof(Archive));
            }
        }

        public IActionResult Archive()
        {
            var archivedBooks = BookService.Instance.GetBooks(includeArchived: true)
                                                   .Where(b => BookService.Instance.GetBooksInternal()
                                                              .FirstOrDefault(book => book.Id == b.BookId)?.IsArchived == true);
            return View(archivedBooks);
        }

        public IActionResult PulloutModal(Guid bookCopyId)
        {
            var bookDetails = BookService.Instance.GetBookDetails(
                BookService.Instance.GetBookCopiesInternal()
                    .FirstOrDefault(bc => bc.Id == bookCopyId)?.Book?.Id ?? Guid.Empty
            );
            
            if (bookDetails == null)
                return NotFound();

            var bookCopy = bookDetails.BookCopies.FirstOrDefault(bc => bc.Id == bookCopyId);
            if (bookCopy == null)
                return NotFound();

            var viewModel = new Library_Management.Models.PulloutBookCopyViewModel
            {
                BookCopyId = bookCopyId,
                BookTitle = bookDetails.Title,
                CoverImageUrl = bookCopy.CoverImageUrl
            };

            return PartialView("_PulloutBookCopyPartial", viewModel);
        }

        [HttpPost]
        public IActionResult PulloutBookCopy(Library_Management.Models.PulloutBookCopyViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                BookService.Instance.PulloutBookCopy(viewModel.BookCopyId, viewModel.PulloutReason!);
                TempData["SuccessMessage"] = "Book copy pulled out successfully!";
                
                // Find the book ID to redirect back to details
                var bookId = BookService.Instance.GetBookCopiesInternal()
                    .FirstOrDefault(bc => bc.Id == viewModel.BookCopyId)?.Book?.Id;
                
                if (bookId.HasValue)
                    return RedirectToAction("Details", new { id = bookId.Value });
                else
                    return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while pulling out the book copy: " + ex.Message;
                return RedirectToAction("Index");
            }
        }


    }
}
