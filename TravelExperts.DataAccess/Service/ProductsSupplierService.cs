

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
