using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperts.DataAccess.Service.IService
{
    public interface IUnitOfWork : IDisposable
    {
        // Adds all the services to the IUnitOfWork interface
        IBookingService Bookings { get; }
        ICustomerService Customers { get; }
        IPackageService Packages { get; }
        IProductsService Products { get; }
        IProductsSupplierService ProductsSuppliers { get; }
        ISupplierService Suppliers { get; }

        // Save method
        Task<int> Save();
    }
}
