using PraktikaAPI.Models;

namespace PraktikaAPI.DAL
{
    public interface IProductTypeRepository
    {
        IEnumerable<ProductType> GetProductTypes();
        ProductType? GetProductTypeByID(int productTypeId);
        void InsertProductType(ProductType productType);
        void DeleteProductType(int productTypeId);
        void UpdateProductType(ProductType productType);
    }
}
