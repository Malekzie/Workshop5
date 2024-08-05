using System.Linq.Expressions;

namespace TravelExperts.DataAccess.Service.IService
{
    public interface IPackageService : IService<Package>
    {
        List<Package> GetById(int id);
        Task<Package> GetFirstOrDefaultAsync(Expression<Func<Package, bool>> filter);
    }
}
