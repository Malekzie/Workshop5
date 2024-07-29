namespace TravelExperts.DataAccess.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly TravelExpertsContext _context;
        public CustomerRepository(TravelExpertsContext context) : base(context)
        {
            _context = context;
        }
    }
}
