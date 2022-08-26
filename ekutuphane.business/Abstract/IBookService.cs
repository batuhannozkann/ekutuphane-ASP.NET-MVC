using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ekutuphane.entity;

namespace ekutuphane.business.Abstract
{
    public interface IBookService
    {
        Book GetById(int id);
        List<Book> GetAll();
        void Create(Book entity);
        void Update(Book entity);
        void Remove(Book Entity);
        Book GetbookdetailWithById(int id);
        List<Book> GetbookByCategories(string category);
        public List<Book> SearchBooks(string search);
        public void Update(Book book,int[] categoryId);
        public void DeleteBookinCategory(int BookId,int CategoryId);
    }
}