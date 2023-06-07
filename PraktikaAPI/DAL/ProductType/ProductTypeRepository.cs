using Microsoft.EntityFrameworkCore;
using PraktikaAPI.Models;

namespace PraktikaAPI.DAL
{
    public class ProductTypeRepository : IProductTypeRepository
    {
        private PraktikaDbContext _context;

        public ProductTypeRepository(PraktikaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductType> GetProductTypes()
        {
            return _context.ProductTypes.
                Include(p => p.ProductGroup).
                Include(p => p.Products).ToList();
        }

        public ProductType? GetProductTypeByID(int productTypeId)
        {
            return _context.ProductTypes.
                Include(p => p.ProductGroup).
                Include(p => p.Products).FirstOrDefault(p => p.ProductTypeId == productTypeId);
        }

        public void InsertProductType(ProductType productType)
        {
            _context.ProductTypes.Add(productType);
            _context.SaveChanges();
        }

        public void DeleteProductType(int productTypeId)
        {
            ProductType? productType = _context.ProductTypes.
                Include(p => p.ProductGroup).
                Include(p => p.Products).FirstOrDefault(p => p.ProductTypeId == productTypeId);

            if (productType != null)
            {
                _context.ProductTypes.Remove(productType);
                _context.SaveChanges();
            }
        }

        public void UpdateProductType(ProductType productType)
        {
            _context.Entry(productType).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
