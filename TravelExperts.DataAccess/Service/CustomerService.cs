

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
        public static Customer GetAccount(TravelExpertsContext db, int customerId)
        {
            Customer? customer = db.Customers.First(c => c.CustomerId == customerId);
            return customer;
        }
    }
}
