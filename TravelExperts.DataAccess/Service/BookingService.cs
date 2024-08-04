using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperts.DataAccess.Service
{
    public class BookingService: Service<Booking>, IBookingService
    {
        private readonly TravelExpertsContext _context;
        public BookingService(TravelExpertsContext context) : base(context)
        {
            _context = context;
        }
    }
}
