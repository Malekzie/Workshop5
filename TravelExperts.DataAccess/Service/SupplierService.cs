using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExperts.Models;

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
