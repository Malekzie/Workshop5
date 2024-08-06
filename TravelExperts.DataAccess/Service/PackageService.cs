

using System.Linq.Expressions;

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

        public async Task<Package> GetFirstOrDefaultAsync(Expression<Func<Package, bool>> filter)
        {
            return await _context.Packages.FirstOrDefaultAsync(filter);
        }
        public decimal? GetPackagePrice(int id)
        {
            decimal? price = _context.Packages.First(p => p.PackageId == id).PkgBasePrice;
            price += _context.Packages.First(p => p.PackageId == id).PkgAgencyCommission;
            return price;
        }

    }

}
