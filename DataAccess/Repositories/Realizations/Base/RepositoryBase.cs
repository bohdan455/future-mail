using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Query;

namespace DataAccess.Repositories.Realizations.Base
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly DataContext _context;

        public RepositoryBase(DataContext context)
        {
            _context = context;
        }
        public IQueryable<T> Query(Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null
            )
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
                query = includes(query);
            if (filter != null)
                query = query.Where(filter);

            return query;

        }
        public IEnumerable<T> GetAll(Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null)
        {
            return Query(includes: includes);
        }
        public IEnumerable<T> GetByCondition(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null)
        {
            return Query(filter: filter, includes: includes);
        }

        public T? GetFirstByCondition(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null)
        {
            return Query(filter: filter, includes: includes).FirstOrDefault();
        }
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public void Insert(T entity)
        {
            _context.Set<T>().Add(entity);
        }
    }
}