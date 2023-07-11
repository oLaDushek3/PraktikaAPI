using PraktikaAPI.Models;

namespace PraktikaAPI.DAL
{
    public interface ISupplyRepository
    {
        IEnumerable<Supply> GetSupplys();
        IEnumerable<SupplyProduct> GetSupplyProducts();
        Supply? GetSupplyByID(int supplyId);
        SupplyProduct? GetSupplyProductByID(int supplyProductId);
        void InsertSupply(Supply supply);
        void InsertSupplyProduct(SupplyProduct supplyProduct);
        bool DeleteSupply(int supplyId);
        bool DeleteSupplyProduct(int supplyProductId);
        void UpdateSupply(Supply supply);
        void UpdateSupplyProduct(SupplyProduct supplyProduct);
    }
}
