using Microsoft.EntityFrameworkCore;
using PraktikaAPI.Models;

namespace PraktikaAPI.DAL
{
    public class SupplyRepository : ISupplyRepository
    {
        private PraktikaDbContext _context;

        public SupplyRepository(PraktikaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Supply> GetSupplys()
        {
            return _context.Supplies.
                Include(s => s.SupplyProducts).
                    ThenInclude(s => s.Orders).ToList();
        }

        public Supply? GetSupplyByID(int supplyId)
        {
            return _context.Supplies.
                Include(s => s.SupplyProducts).
                    ThenInclude(s => s.Orders).FirstOrDefault(s => s.SupplyId == supplyId);
        }

        public void InsertSupply(Supply supply)
        {
            _context.Supplies.Add(supply);
            _context.SaveChanges();
        }

        public void DeleteSupply(int supplyId)
        {
            Supply? supply = _context.Supplies.
                Include(s => s.SupplyProducts).
                    ThenInclude(s => s.Orders).FirstOrDefault(s => s.SupplyId == supplyId);

            if (supply != null)
            {
                _context.Supplies.Remove(supply);
                _context.SaveChanges();
            }
        }

        public void UpdateSupply(Supply supply)
        {
            _context.Entry(supply).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
