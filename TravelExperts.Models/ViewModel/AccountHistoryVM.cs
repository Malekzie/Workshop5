using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExperts.DataAccess.Models;

namespace TravelExperts.Models.ViewModel
{
    public class AccountHistoryVM
    {
        public Customer Customers { get; set; }
        public IEnumerable<Booking> Bookings { get; set; }
    }
}
