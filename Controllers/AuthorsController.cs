using Microsoft.AspNetCore.Mvc;
using ProjectZero.Models;
using ProjectZero.Services;

namespace ProjectZero.Controllers {
    public class AuthorsController : Controller {

        private readonly IAuthorService _service;
        private readonly IAuthorBookConnectionService _service2;
        public AuthorsController (IAuthorService service, IAuthorBookConnectionService service2) {
            this._service = service;
            this._service2 = service2;
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