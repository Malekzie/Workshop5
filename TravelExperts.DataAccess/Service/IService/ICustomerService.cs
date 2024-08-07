
namespace TravelExperts.DataAccess.Repository.IRepository
{
    public interface ICustomerService : IService<Customer>
    {
        // Registers a customer
        void RegisterCustomer(Customer customer);
        
        // Gets account information
        Customer GetAccount(int customerId);
        
        // Gets customer by username
        Customer GetCustomerByUsername(string username);

        // Validates Customer
        Task<Customer> ValidateCustomer(string username, string password);
    }
}
