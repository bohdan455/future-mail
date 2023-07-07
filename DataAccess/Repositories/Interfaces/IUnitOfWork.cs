namespace DataAccess.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        IRepositoryBase<T> Repository<T>() where T : class;
    }
}