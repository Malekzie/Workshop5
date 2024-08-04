
namespace TravelExperts.Models.ViewModel
{
    public class BuyPackageViewModel
    {
        public Package Package { get; set; }
        public IEnumerable<TripType> TripTypes { get; set; }
        public Booking Booking { get; set; }
    }
}
