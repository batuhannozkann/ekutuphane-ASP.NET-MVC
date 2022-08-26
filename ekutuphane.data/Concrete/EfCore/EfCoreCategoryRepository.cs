using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ekutuphane.data.Abstract;
using ekutuphane.entity;
using Microsoft.EntityFrameworkCore;

namespace ekutuphane.data.Concrete.EfCore
{
    public class EfCoreCategoryRepository : EfCoreRepository<Category>, ICategoryRepository
    {
        public EfCoreCategoryRepository(LibraryContext context) : base(context){}
        private LibraryContext LibContext{
            get{return context as LibraryContext;}
        }
        public Category Getcategorybyname(string category)
        {
                return LibContext.Categories.Where(c=>c.CategoryName.ToLower()==category.ToLower()).Include(c=>c.BookCategories).ThenInclude(e=>e.Book).FirstOrDefault();
        }
    }
}