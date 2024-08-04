

namespace TravelExperts.DataAccess.Service
{
    public class PackageService : Service<Package>, IPackageService
    {
        private readonly TravelExpertsContext _context;
        public PackageService(TravelExpertsContext context) : base(context)
        {
            _context = context;
        }
    }

}
