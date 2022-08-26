using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ekutuphane.data.Abstract;
using ekutuphane.entity;
using Microsoft.EntityFrameworkCore;

namespace ekutuphane.data.Concrete.EfCore
{
    public class EfCoreBookRepository : EfCoreRepository<Book>, IBookRepository
    {
        public EfCoreBookRepository(LibraryContext context) : base(context)
        {}
        private LibraryContext LibContext
        {
            get{return context as LibraryContext;}
        }

        public void DeleteBookinCategory(int BookId, int CategoryId)
        {
                const string cmd="delete from BooksCategories where BookId=@p0 and CategoryId=@p1";
                LibContext.Database.ExecuteSqlRaw(cmd,BookId,CategoryId);
                context.SaveChanges();
        }

        public List<Book> GetbookByCategories(string category)
        {
                var books=LibContext.Books.AsQueryable();
                if(!string.IsNullOrEmpty(category))
                {
                    books=books.Include(b=>b.BookCategories).ThenInclude(b=>b.Category).Where(b=>b.BookCategories.Any(c=>c.Category.CategoryName.ToLower()==category.ToLower()));
                }
                return books.ToList();
        }

        public Book GetbookdetailWithById(int id)
        {
           var book=LibContext.Books.Where(b=>b.BookId==id).Include(b=>b.BookCategories).ThenInclude(b=>b.Category).FirstOrDefault();
            return book ?? null;
        }

        public List<Book> GetBooksWithCategories()
        {
                var books=LibContext.Books.Include(b=>b.BookCategories).ThenInclude(b=>b.Category).AsQueryable();
                return books.ToList();
        }

        public List<Book> RecommendedBook()
        {
            var books = LibContext.Books.Where(b=>b.Recommended).ToList();
            return books;
        }

        public List<Book> SearchBooks(string search)
        {
                var books = LibContext.Books.AsQueryable();
                if(!string.IsNullOrEmpty(search))
                {
                    books=books.Where(b=>b.BookName.ToLower().Contains(search)).AsQueryable();
                }
                return books.ToList();
        }
        public void Update(Book book,int[] categoryId)
        {
                var getbook=LibContext.Books.Include(b=>b.BookCategories).FirstOrDefault(b=>b.BookId==book.BookId);
                if(book!=null)
                {
                    getbook.BookName=book.BookName;
                    getbook.AuthorName=book.AuthorName;
                    getbook.BookPage=book.BookPage;
                    getbook.Description=book.Description;
                    getbook.BookCategories=categoryId.Select(c=>new BookCategory{
                BookId=book.BookId,
                CategoryId=c
            }).ToList();
                    getbook.ImageUrl=book.ImageUrl;
                }
                context.SaveChanges();
        }
    }
}