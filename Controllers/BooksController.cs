using Microsoft.AspNetCore.Mvc;
using ProjectZero.Models;
using ProjectZero.Services;

namespace ProjectZero.Controllers {
    public class BooksController : Controller {
        private readonly IBookService _service;
        public BooksController (IBookService service) {
            this._service = service;
        }

        public IActionResult Index() {
            return View("Index",this._service.ShowBooks());
        }

        [HttpPost]
        public IActionResult AddBook(BookModel newBook) {
            this._service.AddBook(newBook);
            return RedirectToAction("Index");
        }

        public IActionResult RemoveBook(int removedBook) {
            this._service.RemoveBook(removedBook);
            return RedirectToAction("Index");
        }
    }
}