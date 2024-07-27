using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperts.DataAccess.Repository
{
    public class ProductRepository: Repository<Product>, IProductRepository
    {
        private readonly TravelExpertsContext _db;
        public ProductRepository(TravelExpertsContext db): base(db)
        {
            _db = db;
        }
    }
}
