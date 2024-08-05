
namespace TravelExperts.DataAccess.Repository.IRepository
{
    public interface ICustomerService : IService<Customer>
    {
        // Registers a customer
        void RegisterCustomer(Customer customer);

        // Add more methods here
        Customer GetAccount(int customerId);
    }
}
