using System.ComponentModel.DataAnnotations;

namespace TravelExperts.DataAccess.Models.ViewModel
{
    public class BuyPackageViewModel
    {
        public int PackageId { get; set; }

        public string PackageName { get; set; }

        public string PackageDesc { get; set; }

        [Required]
        public int BookingId { get; set; }

        public DateTime BookingDate { get; set; }

        public string BookingNo { get; set; }

        public int TravelerCount { get; set; }
        public int CustomerId { get; set; }

        public string TripTypeId { get; set; }
    }
}
