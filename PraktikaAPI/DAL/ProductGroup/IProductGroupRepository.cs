using PraktikaAPI.Models;

namespace PraktikaAPI.DAL
{
    public interface IProductGroupRepository
    {
        IEnumerable<ProductGroup> GetProductGroups();
        ProductGroup? GetProductGroupByID(int productGroupId);
        void InsertProductGroup(ProductGroup productGroup);
        void DeleteProductGroup(int productGroupId);
        void UpdateProductGroup(ProductGroup productGroup);
    }
}
