using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectZero.Models;

namespace ProjectZero.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<BookModel> Books { get; set; }
        public DbSet<AuthorModel> Authors { get; set; }
        public DbSet<LibraryConnectionModel> Connections { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
