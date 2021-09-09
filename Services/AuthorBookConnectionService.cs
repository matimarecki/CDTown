using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectZero.Data;
using ProjectZero.Models;

namespace ProjectZero.Services {
    public interface IAuthorBookConnectionService {
        public void AddAuthorBookConnection(int authorId, int bookId);
        public void RemoveAuthorBookConnection(int removeId);
        public List<LibraryConnectionModel> ShowAuthorBookConnections();
        public bool CheckConnection (int checkAuthor, int checkBook);
        public void RemoveConnectionsAndAuthor(int authorId);
    }
    public class AuthorBookConnectionService : IAuthorBookConnectionService {
        private readonly IAuthorService _service1;
        private readonly IBookService _service2;
        private readonly ApplicationDbContext _dbContext;
        public AuthorBookConnectionService(IAuthorService service1, IBookService service2, ApplicationDbContext dbContext) {
            this._service1 = service1;
            this._service2 = service2;
            this._dbContext = dbContext;
        }

        public void AddAuthorBookConnection(int authorId, int bookId) {
            LibraryConnectionModel newConnection = new LibraryConnectionModel();
            newConnection.Author = this._service1.ShowAuthors().FirstOrDefault(n =>
                n.Id == authorId);
            newConnection.Book = this._service2.ShowBooks().FirstOrDefault(n =>
                n.Id == bookId);
            Console.WriteLine(newConnection.Author.FirstName);
            Console.WriteLine(newConnection.Book.Name);
            this._dbContext.Connections.Add(newConnection);
            this._dbContext.SaveChanges();
        }

        public bool CheckConnection(int checkAuthor, int checkBook) {
            return _dbContext.Connections
                .Any(n => (n.Author.Id == checkAuthor) && (n.Book.Id) == checkBook);
        }

        public void RemoveAuthorBookConnection(int removeId) {
            LibraryConnectionModel removeConnection = new LibraryConnectionModel();
            removeConnection = this._dbContext.Connections
                .FirstOrDefault(n => n.Id == removeId);
            this._dbContext.Remove(removeConnection);
            this._dbContext.SaveChanges();
        }

        public void RemoveConnectionsAndAuthor(int authorId) {
            List<LibraryConnectionModel> hitList = new List<LibraryConnectionModel>();
            hitList = this._dbContext.Connections.Where(n => n.Author.Id == authorId).ToList();
            foreach (var deletingConnection in hitList) {
                this._dbContext.Connections.Remove(deletingConnection);
            }

            this._dbContext.SaveChanges();
        }

        public List<LibraryConnectionModel> ShowAuthorBookConnections() {
            return (this._dbContext.Connections
                .Include(n => n.Author)
                .Include(n => n.Book)
                .ToList());
        }
        
    }
}