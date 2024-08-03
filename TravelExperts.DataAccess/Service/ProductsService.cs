using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
