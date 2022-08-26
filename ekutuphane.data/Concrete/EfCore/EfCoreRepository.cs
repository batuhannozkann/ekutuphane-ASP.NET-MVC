using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ekutuphane.data.Abstract;
using Microsoft.EntityFrameworkCore;

namespace ekutuphane.data.Concrete.EfCore
{
    public class EfCoreRepository<TEntity> : IRepository<TEntity>
    where TEntity:class
    {
        protected readonly DbContext context;
        public EfCoreRepository(DbContext _context)
        {
            context=_context;
        }
        public void Create(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            context.SaveChanges();
        }

        public List<TEntity> GetAll()
        {
                return context.Set<TEntity>().ToList();
        }

        public TEntity GetById(int id)
        {
                return context.Set<TEntity>().Find(id);
        }

        public void Remove(TEntity Entity)
        {
            context.Set<TEntity>().Remove(Entity);
            context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
                context.Entry(entity).State=EntityState.Modified;
                context.SaveChanges();
        }
    }
}