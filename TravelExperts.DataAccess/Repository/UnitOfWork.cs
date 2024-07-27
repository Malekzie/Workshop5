namespace TravelExperts.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TravelExpertsContext _context;

        public UnitOfWork(TravelExpertsContext context)
        {
            _context = context;
            Customer = new CustomerRepository(_context);
            Package = new PackageRepository(_context);
        }


        public ICustomerRepository Customer { get; private set; }
        public IPackageRepository Package { get; private set; }


        public async Task<int> CompleteAsyc()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
