using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ekutuphane.entity;

namespace ekutuphane.data.Abstract
{
    public interface ICategoryRepository:IRepository<Category>
    {
        Category Getcategorybyname(string category);
    }
}