using PraktikaAPI.Models;

namespace PraktikaAPI.DAL
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        Product? GetProductByID(int productId);
        void InsertProduct(Product product);
        void DeleteProduct(int productId);
        void UpdateProduct(Product product);
    }
}
