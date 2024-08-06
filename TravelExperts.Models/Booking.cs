using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TravelExperts.DataAccess.Models
{
    public partial class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [DisplayName("Date Booked")]
        public DateTime? BookingDate { get; set; }

        [DisplayName("Booking Number")]
        public string? BookingNo { get; set; }

        [DisplayName("Traveler Count")]
        [Required(ErrorMessage = "Traveler count is required")]
        [Range(1, double.MaxValue, ErrorMessage = "Traveler count must be at least 1.")]
        public double? TravelerCount { get; set; }

        public int? CustomerId { get; set; }

        [DisplayName("Trip Type")]
        [StringLength(1)]
        [Required(ErrorMessage = "Trip type is required")]
        public string? TripTypeId { get; set; }

        [DisplayName("Package ID")]
        [Required(ErrorMessage = "Package ID is required")]
        public int? PackageId { get; set; }
        
    }
}
