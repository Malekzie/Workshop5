namespace TravelExperts.DataAccess.Repository.IRepository
{
    public interface ICustomerService : IService<Customer>
    {
        Customer GetCustomerByUsernameAndPassword(string username, string password);
        void RegisterCustomer(Customer customer);
    }
}
