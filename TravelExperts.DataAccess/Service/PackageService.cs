

namespace TravelExperts.DataAccess.Service
{
    public class PackageService : Service<Package>, IPackageService
    {
        private readonly TravelExpertsContext _context;
        public PackageService(TravelExpertsContext context) : base(context)
        {
            _context = context;
        }

        public List<Package> GetById(int id)
        {
            return _context.Packages.Where(p => p.PackageId == id).ToList();
        }
    }

}
