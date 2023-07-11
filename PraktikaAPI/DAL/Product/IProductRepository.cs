using PraktikaAPI.Models;

namespace PraktikaAPI.DAL
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        IEnumerable<PriceCategory> GetPriceCategories();
        Product? GetProductByID(int productId);
        void InsertProduct(Product product);
        void InsertProductPriceCategory(ProductPriceCategory productPriceCategory);
        void DeleteProduct(int productId);
        void DeleteProductPriceCategory(int productId);
        void UpdateProduct(Product product);
    }
}
