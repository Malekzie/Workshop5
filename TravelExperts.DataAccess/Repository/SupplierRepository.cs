using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperts.DataAccess.Repository
{
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        private readonly TravelExpertsContext _db;
        public SupplierRepository(TravelExpertsContext db) : base(db)
        {
            _db = db;
        }
    }
}
