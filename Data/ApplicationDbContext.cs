using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectZero.Areas.FileExplorerAdmin.Models;
using ProjectZero.Areas.FileExplorerUser.Models;
using ProjectZero.Models;

namespace ProjectZero.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<BookModel> Books { get; set; }
        public DbSet<AuthorModel> Authors { get; set; }
        public DbSet<LibraryConnectionModel> Connections { get; set; }
        public DbSet<FolderModel> Folders { get; set; }
        public DbSet<FileUploaderModel> FilesUploaded { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)         
        {
            base.OnModelCreating(builder);
            builder.Entity<FolderModel>()
                .HasOne(t => t.Parent);
        }

    }
}
