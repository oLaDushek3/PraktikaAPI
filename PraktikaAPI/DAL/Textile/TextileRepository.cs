using Microsoft.EntityFrameworkCore;
using PraktikaAPI.Models;

namespace PraktikaAPI.DAL
{
    public class TextileRepository : ITextileRepository
    {
        private PraktikaDbContext _context;

        public TextileRepository(PraktikaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Textile> GetTextiles()
        {
            return _context.Textiles.
                Include(t => t.PriceCategory).
                    ThenInclude(p => p.ProductPriceCategories).
                        ThenInclude(p => p.Product).ToList();
        }

        public Textile? GetTextileByID(int textileId)
        {
            return _context.Textiles.
                Include(t => t.PriceCategory).
                    ThenInclude(p => p.ProductPriceCategories).
                        ThenInclude(p => p.Product).FirstOrDefault(t => t.TextileId == textileId);
        }

        public void InsertTextile(Textile textile)
        {
            _context.Textiles.Add(textile);
            _context.SaveChanges();
        }

        public void DeleteTextile(int textileId)
        {
            Textile? textile = _context.Textiles.
                Include(t => t.PriceCategory).
                    ThenInclude(p => p.ProductPriceCategories).
                        ThenInclude(p => p.Product).FirstOrDefault(t => t.TextileId == textileId);

            if (textile != null)
            {
                _context.Textiles.Remove(textile);
                _context.SaveChanges();
            }
        }

        public void UpdateTextile(Textile textile)
        {
            _context.Entry(textile).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
