using Nithin2003.Models;

namespace Nithin2003.Interfaces
{
    public interface IProductService
    {
        public IEnumerable<Product> GetProducts();
        public Product GetProduct(string sku);
    }
}
