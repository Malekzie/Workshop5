

namespace TravelExperts.DataAccess.Service
{
    public class SupplierService : Service<Supplier>, ISupplierService
    {
        private readonly TravelExpertsContext _context;
        public SupplierService(TravelExpertsContext context) : base(context)
        {
            _context = context;
        }
    }
}
