using Microsoft.EntityFrameworkCore;
using PraktikaAPI.Models;

namespace PraktikaAPI.DAL
{
    public class ProductGroupRepository : IProductGroupRepository
    {
        private PraktikaDbContext _context;

        public ProductGroupRepository(PraktikaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductGroup> GetProductGroups()
        {
            return _context.ProductGroups.
                Include(p => p.ProductTypes).ToList();
        }

        public ProductGroup? GetProductGroupByID(int productGroupId)
        {
            return _context.ProductGroups.
                Include(p => p.ProductTypes).FirstOrDefault(p => p.ProductGroupId == productGroupId);
        }

        public void InsertProductGroup(ProductGroup productGroup)
        {
            _context.ProductGroups.Add(productGroup);
            _context.SaveChanges();
        }

        public void DeleteProductGroup(int productGroupId)
        {
            ProductGroup? productGroup = _context.ProductGroups.
                Include(p => p.ProductTypes).FirstOrDefault(p => p.ProductGroupId == productGroupId);

            if (productGroup != null)
            {
                _context.ProductGroups.Remove(productGroup);
                _context.SaveChanges();
            }
        }

        public void UpdateProductGroup(ProductGroup productGroup)
        {
            _context.Entry(productGroup).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
