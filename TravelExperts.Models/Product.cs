#nullable disable
namespace TravelExperts.DataAccess.Models

{

    public partial class Product
    {
        public int ProductId { get; set; }

        public string ProdName { get; set; }

        public virtual ICollection<ProductsSupplier> ProductsSuppliers { get; set; } = new List<ProductsSupplier>();
    }

}