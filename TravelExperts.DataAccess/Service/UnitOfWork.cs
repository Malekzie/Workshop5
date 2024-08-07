
namespace TravelExperts.DataAccess.Service
{
    public class UnitOfWork: IUnitOfWork
    {
        // DB context
        private readonly TravelExpertsContext _context;
        private readonly PasswordHasher<Customer> _passwordHasher;
        public UnitOfWork(TravelExpertsContext context)
        {
            _context = context;
            Packages =  new PackageService(_context);
            Customers = new CustomerService(_context, _passwordHasher);
            Products = new ProductsService(_context);
            ProductsSuppliers = new ProductsSupplierService(_context);
            Suppliers = new SupplierService(_context);
            Bookings = new BookingService(_context);
        }
        
        public IBookingService Bookings { get; private set; }
        public ICustomerService Customers { get; private set; }
        public IProductsService Products { get; private set; }
        public IProductsSupplierService ProductsSuppliers { get; private set; }
        public ISupplierService Suppliers { get; private set; }
        public IPackageService Packages { get; private set; }

        public Task<int> Save()
        {
            // Save changes to the database
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            // Disposes the connection after the operation
            _context.Dispose();
        }
    }
}
