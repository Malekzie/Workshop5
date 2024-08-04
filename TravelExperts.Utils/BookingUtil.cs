using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperts.Utils
{
    public class BookingUtil
    {
        public static string GenerateBookingNumber()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 6);
        }
    }
}
