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
        [Required]
        public int BookingId { get; set; }

        [Display(Name = "Date Booked")]
        public DateTime BookingDate { get; set; }

        [Display(Name = "Booking Number")]
        public string ?BookingNo { get; set; }

        [Display(Name = "Traveler Count")]
        public double ?TravelerCount { get; set; }

        public int ?CustomerId { get; set; }

        [Display(Name = "Trip Type ID")]
        [StringLength(1)]
        public string ?TripTypeId { get; set; }

        [Display(Name = "Package ID")]
        public int ?PackageId { get; set; }
    }
}
    

