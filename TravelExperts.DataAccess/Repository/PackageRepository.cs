using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperts.DataAccess.Repository
{
    public class PackageRepository: Repository<Package>, IPackageRepository
    {
        private readonly TravelExpertsContext _db;
        public PackageRepository(TravelExpertsContext db): base(db)
        {
            _db = db;
        }
    }
}
