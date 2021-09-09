using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectZero.Data;
using ProjectZero.Models;

namespace ProjectZero.Services {
    public interface IAuthorService {
        public void AddAuthor(AuthorModel newAuthor);
        public void RemoveAuthor(int removeAuthor);
        public List<AuthorModel> ShowAuthors();
    }
    public class AuthorService : IAuthorService {
        private readonly ApplicationDbContext dbContext;
        public AuthorService(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }
        public void AddAuthor(AuthorModel newAuthor) {
            dbContext.Authors.Add(newAuthor);
            dbContext.SaveChanges();
        }

        public void RemoveAuthor(int removeAuthor) {
            AuthorModel burningAuthor = this.dbContext.Authors
                .Include(n => n.Connections)
                .FirstOrDefault(n => n.Id == removeAuthor);
            dbContext.Remove(burningAuthor);
            dbContext.RemoveRange(burningAuthor.Connections);
            dbContext.SaveChanges();
        }

        public List<AuthorModel> ShowAuthors() {
            return this.dbContext.Authors.ToList();
        }
    }
}