using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ekutuphane.entity;

namespace ekutuphane.data.Abstract
{
    public interface IBookRepository : IRepository<Book>
    {
        Book GetbookdetailWithById(int id);
        List<Book> GetbookByCategories(string category);

        List<Book> SearchBooks(string search);
        List<Book> GetBooksWithCategories();
        public void Update(Book book,int[] categoryId);
        public void DeleteBookinCategory(int BookId,int CategoryId);
        List<Book> RecommendedBook();
    }
}