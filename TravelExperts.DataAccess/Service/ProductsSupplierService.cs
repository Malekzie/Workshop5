using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperts.DataAccess.Service
{
    public class ProductsSupplierService : Service<ProductsSupplier>, IProductsSupplierService
    {
        private readonly TravelExpertsContext _context;
        public ProductsSupplierService(TravelExpertsContext context) : base(context)
        {
            _context = context;
        }

    }
}
