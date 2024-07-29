using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperts.DataAccess.Repository
{
    public class PackagesProductsSupplierRepository : Repository<PackagesProductsSupplier>, IPackagesProductsSupplierRepository
    {
        private readonly TravelExpertsContext _db;
        public PackagesProductsSupplierRepository(TravelExpertsContext db) : base(db)
        {
            _db = db;
        }
    }
}
