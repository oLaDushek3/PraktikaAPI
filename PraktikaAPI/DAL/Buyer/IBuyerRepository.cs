using PraktikaAPI.Models;

namespace PraktikaAPI.DAL
{
    public interface IBuyerRepository
    {
        IEnumerable<Buyer> GetBuyers();
        Buyer? GetBuyerByID(int buyerId);
        void InsertBuyer(Buyer buyer);
        void DeleteBuyer(int buyerId);
        void UpdateBuyer(Buyer buyer);
    }
}
