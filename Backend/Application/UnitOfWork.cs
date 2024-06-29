using Data.DbContexts;
using Data.Interfaces;
using Data.Models;
using Data.Repositories;

namespace Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        private Dictionary<string, object> _repositories;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            _repositories = new Dictionary<string, object>();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IBaseRepositories<T> GetRepositories<T>() where T : BaseEntity
        {
           if (_repositories.ContainsKey(typeof(T).Name))
           {
                return (IBaseRepositories<T>)_repositories[typeof(T).Name];
           }
           var nRepo = new BaseRepositorie<T>(_context);
            _repositories[typeof(T).Name] = nRepo;
            return nRepo;

        }
    }
}
