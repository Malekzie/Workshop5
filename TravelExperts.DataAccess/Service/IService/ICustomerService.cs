namespace TravelExperts.DataAccess.Repository.IRepository
{
    public interface ICustomerService : IService<Customer>
    {
        void RegisterCustomer(Customer customer);
    }
}
