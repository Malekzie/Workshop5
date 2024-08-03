using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TravelExperts.DataAccess.Service
{
    public class PackageService : Service<Package>, IPackageService
    {
        private readonly TravelExpertsContext _context;
        public PackageService(TravelExpertsContext context) : base(context)
        {
            _context = context;
        }
    }

}
