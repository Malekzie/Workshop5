using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExperts.DataAccess.Service.IService;

namespace TravelExperts.DataAccess.Service
{
    public class CustomerService : Service<Customer>, ICustomerService
    {
        private readonly TravelExpertsContext _context;
        public CustomerService(TravelExpertsContext context) : base(context)
        {
            _context = context;
        }

        public void RegisterCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }
    }
}
