using Data.Models;


namespace Data.Interfaces
{
    public interface IBaseRepositories<T> where T : BaseEntity
    {
        IQueryable<T> Get();
        IQueryable<T> GetPaginated(int pageNumber, int pageSize);
        Task<T> Add(T entity);
        Task<IEnumerable<T>> AddRange(IEnumerable<T> entities);
        Task Delete(T entity);
        Task DeleteRange(IEnumerable<T> entites);
        Task<T> Update(T entity);
        Task<IEnumerable<T>> UpdateRange(IEnumerable<T> entity);

    }
}
