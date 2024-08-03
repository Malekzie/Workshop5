using TravelExperts.DataAccess.Models;

namespace TravelExperts
{
    public static class AccountManager
    {
        public static Customer GetAccount(TravelExpertsContext db, int id)
        {
            Customer? customer = db.Customers.First(c => c.CustomerId == id);
            return customer;
        }
        public static List<Booking> GetOrderHistory(TravelExpertsContext db, int customerId)
        {
            List<Booking> bookingList = db.Bookings.Where(b => b.CustomerId == customerId).ToList();
            return bookingList;
        }


    }
}
