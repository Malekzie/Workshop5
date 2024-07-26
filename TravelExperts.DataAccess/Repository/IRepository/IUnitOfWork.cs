using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperts.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork: IDisposable
    {
        ICustomerRepository Customer { get; }

        Task<int> CompleteAsyc();
    }
}
