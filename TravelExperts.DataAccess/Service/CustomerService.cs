

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

        public Customer GetAccount(int customerId)
        {
            Customer? customer = _context.Customers.First(c => c.CustomerId == customerId);
            return customer;
        }

        public Customer GetCustomerByUsername(string username)
        {
            return _context.Customers.FirstOrDefault(c => c.Username == username);
        }

        public async Task<Customer> ValidateCustomer(string username, string password)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Username == username && c.Password == password);
            return customer;
        }


    }
}
