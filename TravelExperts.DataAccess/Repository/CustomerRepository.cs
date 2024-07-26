using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperts.DataAccess.Repository
{
    public class CustomerRepository: Repository<Customer>, ICustomerRepository 
    {
        private readonly TravelExpertsContext _context;
        public CustomerRepository(TravelExpertsContext context) : base(context)
        {
            _context = context;
        }
    }
}
