namespace TravelExperts.DataAccess.Service.IService
{
    public interface IPackageService : IService<Package>
    {
        List<Package> GetById(int id);
    }
}
