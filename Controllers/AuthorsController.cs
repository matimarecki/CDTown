using Microsoft.AspNetCore.Mvc;
using ProjectZero.Models;
using ProjectZero.Services;

namespace ProjectZero.Controllers {
    public class AuthorsController : Controller {

        private readonly IAuthorService _service;
        public AuthorsController (IAuthorService service) {
            this._service = service;
        }

        public IActionResult Index() {
            return View(this._service.ShowAuthors());
        }

        [HttpPost]
        public IActionResult AddAuthor(AuthorModel newAuthor) {
            this._service.AddAuthor(newAuthor);
            return RedirectToAction("Index");
        }

        public IActionResult RemoveAuthor(int removedAuthor) {
            this._service.RemoveAuthor(removedAuthor);
            return RedirectToAction("Index");
        }
    }
}