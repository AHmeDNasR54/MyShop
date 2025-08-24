using Microsoft.EntityFrameworkCore;
using myShop.DataAccess.Data;
using myShop.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace myShop.DataAccess.Implementation
{
    public class GenericRepository<T>: IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> _dbSet;
        public GenericRepository(ApplicationDbContext context) {
            _context = context;
            _dbSet = context.Set<T>();
        }


        void IGenericRepository<T>.Add(T entity)
        {
            //Categories.Add(category);
            _dbSet.Add(entity);
        }

        IEnumerable<T> IGenericRepository<T>.GetAll(Expression<Func<T, bool>>? perdicate =null , string? Includeword=null )
        {
            IQueryable<T> query =_dbSet;
            if(perdicate!= null)
            {
                query = query.Where(perdicate);
            }
            if(Includeword!= null)
            {
                //_context.Products.Include("Category,Logos,Users)
                foreach (var item in Includeword.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            return query.ToList();

        }

        T IGenericRepository<T>.GetFirstorDefault(Expression<Func<T, bool>>? perdicate =null, string? Includeword = null)
        {
            IQueryable<T> query = _dbSet;
            if (perdicate != null)
            {
                query = query.Where(perdicate);
            }
            if (Includeword != null)
            {
                //_context.Products.Include("Category,Logos,Users)
                foreach (var item in Includeword.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            return query.SingleOrDefault();
        }

        void IGenericRepository<T>.Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        void IGenericRepository<T>.RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
    }
}
