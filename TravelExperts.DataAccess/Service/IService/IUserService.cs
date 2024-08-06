using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExperts.Models;

namespace TravelExperts.DataAccess.Service.IService
{
    public interface IUserService: IService<User>
    {
        // Get user by Username
        User GetUser(string username);
        // Register user
        void RegisterUser(User user);

        Task<User> GetUserById(int customerId);

        // Gets customer through user
        Task<Customer> GetCustomerByID(int userId);
    }
}
