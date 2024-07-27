using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperts.DataAccess.Repository
{
    public class ProductsSupplierRepository : Repository<ProductsSupplier>, IProductsSupplierRepository
    {
        private readonly TravelExpertsContext _db;
        public ProductsSupplierRepository(TravelExpertsContext db) : base(db)
        {
            _db = db;
        }
    }
}
