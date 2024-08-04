

namespace TravelExperts.DataAccess.Service
{
    public class ProductsService: Service<Product>, IProductsService
    {
        private readonly TravelExpertsContext _context;
        public ProductsService(TravelExpertsContext context) : base(context)
        {
            _context = context;
        }
    }
}
