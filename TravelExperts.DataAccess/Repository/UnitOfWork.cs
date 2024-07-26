using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperts.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TravelExpertsContext _context;
        public ICustomerRepository Customer { get; private set; }

        public UnitOfWork(TravelExpertsContext context)
        {
            _context = context;
            Customer = new CustomerRepository(_context);
        }

        public async Task<int> CompleteAsyc()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
