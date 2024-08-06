using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperts.DataAccess.Service.IService
{
    public interface IBookingService: IService<Booking>
    {
       Task<IEnumerable<Booking>> GetOrderHistory(int customerId);
    }
}
