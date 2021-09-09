using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using ProjectZero.Data;
using ProjectZero.Models;

namespace ProjectZero.Services {
    public interface IBookService {
        public void AddBook(BookModel newBook);
        public List<BookModel> ShowBooks();
        public void RemoveBook(int removeBook);
    }
    public class BookService : IBookService {
        private readonly ApplicationDbContext dbContext;

        public BookService(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }
        public void AddBook(BookModel newBook) {
            dbContext.Books.Add(newBook);
            dbContext.SaveChanges();
        }

        public void RemoveBook(int removeBook) {
            BookModel burningBook = ShowBooks().FirstOrDefault(n =>
                n.Id == removeBook);
            dbContext.Books.Remove(burningBook);
            dbContext.SaveChanges();
        }

        public List<BookModel> ShowBooks() {
            return this.dbContext.Books.ToList();
        }
    }
}