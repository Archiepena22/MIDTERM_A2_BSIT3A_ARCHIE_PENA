using Library_Management.Models;
using Library_Management.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management.Controllers  
{   
    public class AuthorController : Controller
    {
        public IActionResult Index()   
        {
            var authors = AuthorService.Instance.GetAuthors(includeArchived: false);
            return View(authors);
        }

        public IActionResult Details(Guid id)
        {
            var author = AuthorService.Instance.GetAuthorById(id);
            if (author == null)
                return NotFound();

            return View(author);
        }

        public IActionResult Create()
        {
            return View(new AddAuthorViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AddAuthorViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            try
            {
                AuthorService.Instance.AddAuthor(viewModel);
                TempData["SuccessMessage"] = "Author added successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while adding the author: " + ex.Message);
                return View(viewModel);
            }
        }

        public IActionResult Edit(Guid id)
        {
            var author = AuthorService.Instance.GetAuthorForEdit(id);
            if (author == null)
                return NotFound();

            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditAuthorViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            try
            {
                AuthorService.Instance.UpdateAuthor(viewModel);
                TempData["SuccessMessage"] = "Author updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while updating the author: " + ex.Message);
                return View(viewModel);
            }
        }

        public IActionResult DeleteModal(Guid id)
        {
            var author = AuthorService.Instance.GetAuthorById(id);
            if (author == null)
                return NotFound();

            return PartialView("_DeleteAuthorPartial", author);
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var canDelete = AuthorService.Instance.CanDeleteAuthor(id);
                if (!canDelete)
                {
                    TempData["ErrorMessage"] = "Cannot delete this author as they have books associated with them. Consider archiving instead.";
                    return RedirectToAction(nameof(Index));
                }

                AuthorService.Instance.DeleteAuthor(id);
                TempData["SuccessMessage"] = "Author deleted successfully!";
                return Ok();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the author: " + ex.Message;
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Archive(Guid id)
        {
            try
            {
                AuthorService.Instance.ArchiveAuthor(id);
                TempData["SuccessMessage"] = "Author archived successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while archiving the author: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public IActionResult Restore(Guid id)
        {
            try
            {
                AuthorService.Instance.RestoreAuthor(id);
                TempData["SuccessMessage"] = "Author restored successfully!";
                return RedirectToAction(nameof(Archive));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while restoring the author: " + ex.Message;
                return RedirectToAction(nameof(Archive));
            }
        }

        public IActionResult Archive()
        {
            var archivedAuthors = AuthorService.Instance.GetAuthors(includeArchived: true)
                                                      .Where(a => a.IsArchived);
            return View(archivedAuthors);
        }
    }
}
