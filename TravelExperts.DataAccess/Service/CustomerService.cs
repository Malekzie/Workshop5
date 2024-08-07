


namespace TravelExperts.DataAccess.Service
{
    public class CustomerService : Service<Customer>, ICustomerService
    {
        private readonly TravelExpertsContext _context;
        private readonly PasswordHasher<Customer> _passwordHasher;
        public CustomerService(TravelExpertsContext context, PasswordHasher<Customer> passwordHasher) : base(context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Customer>();
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
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Username == username);

            if (customer == null)
            {
                return null;
            }

            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(customer, customer.Password, password);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                return null;
            }

            return customer;
        }



    }
}
