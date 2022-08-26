using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ekutuphane.business.Abstract;
using ekutuphane.data.Abstract;
using ekutuphane.data.Concrete.EfCore;
using ekutuphane.entity;

namespace ekutuphane.business.Concrete
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private LibraryContext _context;
        public BookService(IUnitOfWork unitOfWork,LibraryContext context)
        {
            _unitOfWork=unitOfWork;
            _context=context;
        }
        public void Create(Book entity)
        {
            _unitOfWork.Books.Create(entity);
        }

        public void DeleteBookinCategory(int BookId, int CategoryId)
        {
            _unitOfWork.Books.DeleteBookinCategory(BookId,CategoryId);
            
        }

        public List<Book> GetAll()
        {
           return _unitOfWork.Books.GetAll();
        }

        public List<Book> GetbookByCategories(string category)
        {
            return _unitOfWork.Books.GetbookByCategories(category);
        }

        public Book GetbookdetailWithById(int id)
        {
            return _unitOfWork.Books.GetbookdetailWithById(id);
        }

        public Book GetById(int id)
        {
            return _unitOfWork.Books.GetById(id);
        }

        public void Remove(Book Entity)
        {
            _unitOfWork.Books.Remove(Entity);
        }

        public List<Book> SearchBooks(string search)
        {
            return _unitOfWork.Books.SearchBooks(search);
        }

        public void Update(Book entity)
        {
            _unitOfWork.Books.Update(entity);
        }

        public void Update(Book book, int[] categoryId)
        {
            _unitOfWork.Books.Update(book,categoryId);
        }
    }
}