using PraktikaAPI.Models;

namespace PraktikaAPI.DAL
{
    public interface ISupplyRepository
    {
        IEnumerable<Supply> GetSupplys();
        Supply? GetSupplyByID(int supplyId);
        void InsertSupply(Supply supply);
        void DeleteSupply(int supplyId);
        void UpdateSupply(Supply supply);
    }
}
