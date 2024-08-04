
namespace TravelExperts.DataAccess.Repository
{

    // Generic Service class that implements IService
    public class Service<T> : IService<T> where T : class
    {
        // DB Context instance
        private readonly TravelExpertsContext _context;

        // DB Set instance
        private DbSet<T> _db;

        public Service(TravelExpertsContext context)
        {
            _context = context;
            _db = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _db.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _db.FindAsync(id);
        }

        public void Add(T entity)
        {
            _db.Add(entity);
        }

        public void Delete(T entity)
        {
            _db.Remove(entity);
        }

        public Task<T> Update(T entity)
        {
            // Update the entity
            _db.Update(entity);

            // Return the entity result after updating
            return Task.FromResult(entity);
        }
    }
}