using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProjectZero.Models;
using ProjectZero.Services;

namespace ProjectZero.Controllers {
    public class ConnectionsController : Controller {
        private readonly IAuthorService _service1;
        private readonly IBookService _service2;
        private readonly IAuthorBookConnectionService _service3;
        public ConnectionsController (IAuthorService service1, IBookService service2, IAuthorBookConnectionService service3) {
            this._service1 = service1;
            this._service2 = service2;
            this._service3 = service3;
        }

        public IActionResult Index() {
            return View(this._service3.ShowAuthorBookConnections());
        }

        public IActionResult CreateNewConnection() {
            LibraryModel sentList = new LibraryModel();
            sentList.AllTheAuthors = this._service1.ShowAuthors();
            sentList.AllTheBooks = this._service2.ShowBooks();
            return View(sentList);
        }

        [HttpPost]
        public IActionResult AddConnection(int authorId, int bookId) {
            if (this._service3.CheckConnection(authorId, bookId)) {
                Console.WriteLine("This connection already exists");
            }
            else {
                this._service3.AddAuthorBookConnection(authorId, bookId);
            }
            return RedirectToAction("CreateNewConnection");
        }

        public IActionResult RemoveConnection(int rmvConnection) {
            this._service3.RemoveAuthorBookConnection(rmvConnection);
            return RedirectToAction("Index");
        }

        public IActionResult CheckConnection(int givenAuthor, int givenBook) {
            if (this._service3.CheckConnection(givenAuthor, givenBook)) {
                Console.WriteLine("Dot dot doT");
            };
            return RedirectToAction("Index");
        }
    }
}