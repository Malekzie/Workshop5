

using TravelExperts.DataAccess.Models;

namespace TravelExperts.DataAccess.Service
{
    public class UserService : Service<User> ,IUserService
    {
        private readonly TravelExpertsContext _context;

        public UserService(TravelExpertsContext context) : base(context)
        {
            _context = context;
        }


        public User GetUser(string username)
        {
            return _context.Users.SingleOrDefault(c => c.Username == username);
        }

        public void RegisterUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public async Task<Customer> GetCustomerByID(int customerId)
        {
            var user = await _context.Users
            .Include(u => u.Customer)
            .FirstOrDefaultAsync(u => u.CustomerId == customerId);

            return user?.Customer;
        }

        public async Task<User> GetUserById(int customerId)
        {
            return _context.Users.SingleOrDefault(c => c.UserId == customerId);
        }
    }
}
