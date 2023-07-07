using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Realizations.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;


        public UnitOfWork(DataContext context)
        {
            _context = context;
        }
        public IRepositoryBase<T> Repository<T>() where T : class
        {
            return new RepositoryBase<T>(_context);
        }
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
