using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace DataAccess.Repositories.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        void Delete(T entity);
        IEnumerable<T> GetAll(Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null);
        IEnumerable<T> GetByCondition(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null);
        T? GetFirstByCondition(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null);
        void Insert(T entity);
        IQueryable<T> Query(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null);
        void Update(T entity);
    }
}