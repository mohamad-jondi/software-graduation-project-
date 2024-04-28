using Data.Models;

namespace Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepositories<T> GetRepositories<T>() where T : BaseEntity;
    }
}
