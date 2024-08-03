using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
