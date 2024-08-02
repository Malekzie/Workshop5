using TravelExperts.DataAccess.Models;

namespace TravelExperts
{
    public static class AccountManager
    {
        public static Customer GetOrderHistory(TravelExpertsContext db)
        {
            Customer customer = db.Customers.FirstOrDefault(c => c.CustomerId == 104);
            return customer;
        }


    }
}
