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
                Include(p => p.SupplyProducts).
                    ThenInclude(p => p.Orders).
                Include(p => p.SupplyProducts).
                    ThenInclude(p => p.Textile).
                Include(p => p.SupplyProducts).
                    ThenInclude(p => p.Product).
                    ThenInclude(p => p.Color).ToList();
        }

        public IEnumerable<SupplyProduct> GetSupplyProducts()
        {
            return _context.SupplyProducts.
                Include(p => p.Supply).
                Include(p => p.Orders).
                Include(p => p.Textile).
                Include(p => p.Product).
                    ThenInclude(p => p.ProductPriceCategories).
                Include(p => p.Product).
                    ThenInclude(p => p.Color).
                Include(p => p.Product).
                    ThenInclude(p => p.ProductType).
                        ThenInclude(p => p.ProductGroup).ToList();
        }

        public Supply? GetSupplyByID(int supplyId)
        {
            return _context.Supplies.
                Include(p => p.SupplyProducts).
                    ThenInclude(p => p.Orders).
                Include(p => p.SupplyProducts).
                    ThenInclude(p => p.Textile).
                Include(p => p.SupplyProducts).
                    ThenInclude(p => p.Product).
                    ThenInclude(p => p.Color).FirstOrDefault(s => s.SupplyId == supplyId);
        }

        public SupplyProduct? GetSupplyProductByID(int supplyProductId)
        {
            return _context.SupplyProducts.
                Include(p => p.Supply).
                Include(p => p.Product).
                    ThenInclude(p => p.Color).
                Include(p => p.Textile).FirstOrDefault(s => s.SupplyId == supplyProductId);
        }

        public void InsertSupply(Supply supply)
        {
            _context.Supplies.Add(supply);
            _context.SaveChanges();
        }

        public void InsertSupplyProduct(SupplyProduct supplyProduct)
        {
            _context.SupplyProducts.Add(supplyProduct);
            _context.SaveChanges();
        }

        public bool DeleteSupply(int supplyId)
        {
            Supply? supply = _context.Supplies.
                Include(s => s.SupplyProducts).
                    ThenInclude(s => s.Orders).
                Include(s => s.SupplyProducts).
                    ThenInclude(s => s.Product).FirstOrDefault(s => s.SupplyId == supplyId);

            if (supply != null)
            {
                _context.Supplies.Remove(supply);
                _context.SaveChanges();
                return true;
            }
            else
                return false;

        }

        public bool DeleteSupplyProduct(int supplyProductId)
        {
            SupplyProduct? supply = _context.SupplyProducts.
                Include(p => p.Supply).
                Include(p => p.Product).
                    ThenInclude(p => p.Color).
                Include(p => p.Textile).FirstOrDefault(s => s.SupplyProductsId == supplyProductId);

            if (supply != null)
            {
                _context.SupplyProducts.Remove(supply);
                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public void UpdateSupply(Supply supply)
        {
            _context.Entry(supply).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void UpdateSupplyProduct(SupplyProduct supplyProduct)
        {
            _context.Entry(supplyProduct).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}