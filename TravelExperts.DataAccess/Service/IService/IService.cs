namespace TravelExperts.DataAccess.Repository.IRepository
{

    public interface IService<T> where T : class
    {
        
        // Basic CRUD methods

        // Get all entities
        Task<IEnumerable<T>> GetAllAsync();

        // Get entity by id
        Task<T> GetByIdAsync(int id);

        // Add entity
        void Add(T entity);

        // Update entity
        Task<T> Update(T entity);

        // Delete entity
        void Delete(T entity);
    }
}
