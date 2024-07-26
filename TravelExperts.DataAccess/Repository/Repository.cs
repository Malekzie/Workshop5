
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace TravelExperts.DataAccess.Repository
{
    public class Repository<T>: IRepository<T> where T : class
    {
        private readonly TravelExpertsContext _context;
        private DbSet<T> _db;

        public Repository(TravelExpertsContext context)
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
            _db.Update(entity);
            return Task.FromResult(entity);
        }
    }
}
