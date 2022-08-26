using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ekutuphane.business.Abstract;
using ekutuphane.data.Abstract;
using ekutuphane.data.Concrete.EfCore;

namespace ekutuphane.business.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryContext _context;
        public UnitOfWork(LibraryContext context)
        {
            _context=context;
        }
        private EfCoreBookRepository _bookRepository;
        private EfCoreCategoryRepository _categoryRepository;
        public IBookRepository Books =>_bookRepository ?? (_bookRepository = new EfCoreBookRepository(_context));

        public ICategoryRepository Categories => _categoryRepository ??(_categoryRepository= new EfCoreCategoryRepository(_context));

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}