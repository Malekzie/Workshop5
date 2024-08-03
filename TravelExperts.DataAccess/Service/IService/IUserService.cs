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
        User GetUser(string username, string password);
        void RegisterUser(User user);
        Task<Customer> GetCustomerByID(int userId);
    }
}
