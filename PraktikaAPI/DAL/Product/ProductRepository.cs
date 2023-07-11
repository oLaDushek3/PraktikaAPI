using Microsoft.EntityFrameworkCore;
using PraktikaAPI.Models;

namespace PraktikaAPI.DAL
{
    public class ProductRepository : IProductRepository
    {
        private PraktikaDbContext _context;

        public ProductRepository(PraktikaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _context.Products.
                Include(p => p.ProductPriceCategories).
                    ThenInclude(p => p.PriceCategory).
                Include(p => p.Color).
                Include(p => p.ProductType).
                    ThenInclude(p => p.ProductGroup).
                Include(p => p.SupplyProducts).
                    ThenInclude(p => p.Supply).
                Include(p => p.SupplyProducts).
                    ThenInclude(p => p.Orders).
                    ToList();
        }

        public IEnumerable<PriceCategory> GetPriceCategories()
        {
            return _context.PriceCategories.ToList();
        }

        public Product? GetProductByID(int productId)
        {
            return _context.Products.
                Include(p => p.ProductPriceCategories).
                    ThenInclude(p => p.PriceCategory).
                Include(p => p.Color).
                Include(p => p.ProductType).
                    ThenInclude(p => p.ProductGroup).
                Include(p => p.SupplyProducts).
                    ThenInclude(p => p.Supply).
                Include(p => p.SupplyProducts).
                    ThenInclude(p => p.Orders).
                    FirstOrDefault(p => p.ProductId == productId);
        }

        public void InsertProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }
        public void InsertProductPriceCategory(ProductPriceCategory productPriceCategory)
        {
            _context.ProductPriceCategorys.Add(productPriceCategory);
            _context.SaveChanges();
        }

        public void DeleteProduct(int productId)
        {
            Product? product = _context.Products.
                Include(p => p.ProductPriceCategories).
                    ThenInclude(p => p.PriceCategory).
                Include(p => p.Color).
                Include(p => p.ProductType).
                    ThenInclude(p => p.ProductGroup).
                Include(p => p.SupplyProducts).
                    ThenInclude(p => p.Supply).
                Include(p => p.SupplyProducts).
                    ThenInclude(p => p.Orders).
                    FirstOrDefault(p => p.ProductId == productId);

            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }
        public void DeleteProductPriceCategory(int productId)
        {
            List<ProductPriceCategory> productPriceCategories = _context.ProductPriceCategorys.Where(p => p.ProductId == productId).ToList();

            foreach(ProductPriceCategory category in productPriceCategories)
            {
                _context.Remove(category);
            }

            _context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
