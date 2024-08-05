using System;
using System.ComponentModel.DataAnnotations;

namespace TravelExperts.DataAccess.Models
{
    public partial class Booking
    {
        [Key]
        public int BookingId { get; set; }

        public DateTime? BookingDate { get; set; }

        public string? BookingNo { get; set; }

        [Required(ErrorMessage = "Traveler count is required")]
        [Range(1, double.MaxValue, ErrorMessage = "Traveler count must be at least 1.")]
        public double? TravelerCount { get; set; }

        public int? CustomerId { get; set; }

        [StringLength(1)]
        [Required(ErrorMessage = "Trip type is required")]
        public string? TripTypeId { get; set; }

        [Required(ErrorMessage = "Package ID is required")]
        public int? PackageId { get; set; }
    }
}
