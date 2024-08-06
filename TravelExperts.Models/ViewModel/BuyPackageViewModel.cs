
namespace TravelExperts.Models.ViewModel
{
    public class BuyPackageViewModel
    {
        public int PackageId { get; set; }
        public IEnumerable<TripType>? TripTypes { get; set; }
        public Booking Booking { get; set; } = new Booking(); // Ensure Booking is initialized
    }
}
