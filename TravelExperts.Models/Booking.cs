using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperts.DataAccess.Models
{
    public partial class Booking
    {
        [Key]
        public int BookingId { get; set; }

        public DateTime ?BookingDate { get; set; }
                      
        public string ?BookingNo { get; set; }

        public double ?TravelerCount { get; set; }

        public int ?CustomerId { get; set; }

        [StringLength(1)]
        public string ?TripTypeId { get; set; }

        public int ?PackageId { get; set; }
    }
}
    

