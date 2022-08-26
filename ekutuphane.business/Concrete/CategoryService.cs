using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ekutuphane.business.Abstract;
using ekutuphane.data.Abstract;
using ekutuphane.entity;

namespace ekutuphane.business.Concrete
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository=categoryRepository;
        }
        public void Create(Category entity)
        {
            _categoryRepository.Create(entity);
        }

        public List<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        public Category GetById(int id)
        {
            return _categoryRepository.GetById(id);
        }

        public Category Getcategorybyname(string category)
        {
            return _categoryRepository.Getcategorybyname(category);
        }

        public void Remove(Category Entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Category entity)
        {
            _categoryRepository.Update(entity);
        }
    }
}