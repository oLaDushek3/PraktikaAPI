using Microsoft.EntityFrameworkCore;
using PraktikaAPI.Models;

namespace PraktikaAPI.DAL
{
    public class ColorRepository : IColorRepository
    {
        private PraktikaDbContext _context;

        public ColorRepository(PraktikaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Color> GetColors()
        {
            return _context.Colors.
                Include(c => c.Products).
                    ThenInclude(p => p.ProductPriceCategories).
                Include(c => c.Products).
                    ThenInclude(p => p.ProductType).
                        ThenInclude(p => p.ProductGroup).
                Include(c => c.Products).
                    ThenInclude(p => p.SupplyProducts).
                        ThenInclude(p => p.Supply).ToList();
        }

        public Color? GetColorByID(int colorId)
        {
            return _context.Colors.
                Include(c => c.Products).
                    ThenInclude(p => p.ProductPriceCategories).
                Include(c => c.Products).
                    ThenInclude(p => p.ProductType).
                        ThenInclude(p => p.ProductGroup).
                Include(c => c.Products).
                    ThenInclude(p => p.SupplyProducts).
                        ThenInclude(p => p.Supply).FirstOrDefault(c => c.ColorId == colorId);
        }

        public void InsertColor(Color color)
        {
            _context.Colors.Add(color);
            _context.SaveChanges();
        }

        public void DeleteColor(int colorId)
        {
            Color? color = _context.Colors.
                Include(c => c.Products).
                    ThenInclude(p => p.ProductPriceCategories).
                Include(c => c.Products).
                    ThenInclude(p => p.ProductType).
                        ThenInclude(p => p.ProductGroup).
                Include(c => c.Products).
                    ThenInclude(p => p.SupplyProducts).
                        ThenInclude(p => p.Supply).FirstOrDefault(c => c.ColorId == colorId);

            if (color != null)
            {
                _context.Colors.Remove(color);
                _context.SaveChanges();
            }
        }

        public void UpdateColor(Color color)
        {
            _context.Entry(color).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
