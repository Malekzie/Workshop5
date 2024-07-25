#nullable disable
using System;
using System.Collections.Generic;

namespace TravelExperts.DataAccess.Models;

public partial class ProductsSupplier
{
    public int ProductSupplierId { get; set; }

    public int? ProductId { get; set; }

    public int? SupplierId { get; set; }

    public virtual ICollection<PackagesProductsSupplier> PackagesProductsSuppliers { get; set; } = new List<PackagesProductsSupplier>();

    public virtual Product Product { get; set; }

    public virtual Supplier Supplier { get; set; }
}