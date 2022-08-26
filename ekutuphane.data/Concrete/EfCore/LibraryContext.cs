using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ekutuphane.data.Configurations;
using ekutuphane.entity;
using Microsoft.EntityFrameworkCore;

namespace ekutuphane.data.Concrete.EfCore
{
    public class LibraryContext:DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options):base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories {get; set;}
        public DbSet<BookCategory> BooksCategories {get ; set;}
        /*
        ----Startup içinden çağırarak connectionstringi gönderdim----
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource=eLibraryDb");
        }*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new BookCategoryConfiguration());
            modelBuilder.Seed();
        }
    }
}