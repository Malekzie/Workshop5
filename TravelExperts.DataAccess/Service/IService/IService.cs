namespace TravelExperts.DataAccess.Repository.IRepository
{
    public interface IService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        void Add(T entity);
        Task<T> Update(T entity);
        void Delete(T entity);
    }
}
