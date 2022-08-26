using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ekutuphane.data.Abstract;
using ekutuphane.data.Concrete.EfCore;

namespace ekutuphane.business.Abstract
{
    public interface IUnitOfWork:IDisposable
    {
        IBookRepository Books{get;}
        ICategoryRepository Categories{get;}
        int Save();
    }
}