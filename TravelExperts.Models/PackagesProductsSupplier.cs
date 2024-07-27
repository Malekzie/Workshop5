#nullable disable

namespace TravelExperts.DataAccess.Models
{
    public partial class PackagesProductsSupplier
    {
        public int PackageProductSupplierId { get; set; }

        public int PackageId { get; set; }

        public int ProductSupplierId { get; set; }

        public virtual Package Package { get; set; }

        public virtual ProductsSupplier ProductSupplier { get; set; }
    }

}