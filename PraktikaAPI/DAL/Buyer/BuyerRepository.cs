using Microsoft.EntityFrameworkCore;
using PraktikaAPI.Models;

namespace PraktikaAPI.DAL
{
    public class BuyerRepository : IBuyerRepository
    {
        private PraktikaDbContext _context;

        public BuyerRepository(PraktikaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Buyer> GetBuyers()
        {
            List<Buyer> buyerList = _context.Buyers.
                Include(b => b.Individual).
                Include(b => b.LegalEntity).
                Include(b => b.Orders).ToList();

            foreach (Buyer item in buyerList)
            {
                Buyer buyer = buyerList.First(b => b.BuyerId == item.BuyerId);
                buyer = BuyerEncryption.Decrypt(buyer);
            }

            if (buyerList.Count > 0)
                return buyerList;
            else
                return null;
        }

        public Buyer? GetBuyerByID(int buyerId)
        {
            Buyer? buyer = _context.Buyers.
                Include(b => b.Individual).
                Include(b => b.LegalEntity).
                Include(b => b.Orders).FirstOrDefault(b => b.BuyerId == buyerId);

            if(buyer != null)
                return (BuyerEncryption.Decrypt(buyer));
            else 
                return null;
        }

        public void InsertBuyer(Buyer buyer)
        {
            _context.Buyers.Add(BuyerEncryption.Encrypt(buyer));
            _context.SaveChanges();
        }

        public void DeleteBuyer(int buyerId)
        {
            Buyer? buyer = _context.Buyers.
                Include(b => b.Individual).
                Include(b => b.LegalEntity).
                Include(b => b.Orders).FirstOrDefault(b => b.BuyerId == buyerId);

            if (buyer != null)
            {
                if(buyer.Individual != null)
                    _context.Individuals.Remove(buyer.Individual);
                else
                    _context.LegalEntities.Remove(buyer.LegalEntity);
                _context.Buyers.Remove(buyer);
                _context.SaveChanges();
            }
        }

        public void UpdateBuyer(Buyer buyer)
        {
            buyer = BuyerEncryption.Encrypt(buyer);

            if (buyer.Individual != null)
                _context.Entry(buyer.Individual).State = EntityState.Modified;
            else
                _context.Entry(buyer.LegalEntity).State = EntityState.Modified;

            _context.Entry(buyer).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
