namespace TravelExperts.DataAccess.Repository.IRepository
{
    // Generic interface for Repository pattern to be implemented by all repositories
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        void Add(T entity);
        Task<T> Update(T entity);
        void Delete(T entity);
    }
}
