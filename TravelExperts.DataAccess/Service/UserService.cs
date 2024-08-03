using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExperts.DataAccess.Models;
using TravelExperts.Models;

namespace TravelExperts.DataAccess.Service
{
    public class UserService : Service<User> ,IUserService
    {
        private readonly TravelExpertsContext _context;

        public UserService(TravelExpertsContext context) : base(context)
        {
            _context = context;
        }


        public User GetUser(string username, string password)
        {
            return _context.Users.SingleOrDefault(c => c.Username == username && c.Password == password);
        }

        public void RegisterUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public async Task<Customer> GetCustomerByID(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Customer)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            return user.Customer;
        }
    }
}
