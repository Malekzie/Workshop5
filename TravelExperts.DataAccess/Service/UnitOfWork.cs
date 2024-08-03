﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperts.DataAccess.Service
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly TravelExpertsContext _context;
        public UnitOfWork(TravelExpertsContext context)
        {
            _context = context;
            Packages =  new PackageService(_context);
            Customers = new CustomerService(_context);
            Products = new ProductsService(_context);
            ProductsSuppliers = new ProductsSupplierService(_context);
            Suppliers = new SupplierService(_context);
        }

        public ICustomerService Customers { get; private set; }
        public IProductsService Products { get; private set; }
        public IProductsSupplierService ProductsSuppliers { get; private set; }
        public ISupplierService Suppliers { get; private set; }
        public IPackageService Packages { get; private set; }

        public Task<int> Save()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
