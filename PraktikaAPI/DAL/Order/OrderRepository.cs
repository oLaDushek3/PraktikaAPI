using Microsoft.EntityFrameworkCore;
using PraktikaAPI.Models;

namespace PraktikaAPI.DAL
{
    public class OrderRepository : IOrderRepository
    {
        private PraktikaDbContext _context;

        public OrderRepository(PraktikaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Order> GetOrders()
        {
            List<Order> orderList = _context.Orders.
                Include(o => o.Employee).
                Include(o => o.SupplyProducts).
                    ThenInclude(s => s.Product).
                        ThenInclude(s => s.Color).
                Include(o => o.SupplyProducts).
                    ThenInclude(s => s.Product).
                        ThenInclude(s => s.ProductType).
                            ThenInclude(s => s.ProductGroup).
                Include(o => o.SupplyProducts).
                    ThenInclude(s => s.Supply).
                Include(o => o.Buyer).
                    ThenInclude(b => b.Individual).
                Include(o => o.Buyer).
                    ThenInclude(b => b.LegalEntity).ToList();

            List<Buyer> buyers = new();
            bool decrypt = true;
            for (int i = 0; i < orderList.Count; i++)
            {
                foreach (var entity in buyers)
                {
                    if (entity.BuyerId == orderList[i].Buyer.BuyerId)
                    {
                        decrypt = false;
                        break;
                    }
                    else
                        decrypt = true;
                }
                if (decrypt)
                {
                    orderList[i].Buyer = BuyerEncryption.Decrypt(orderList[i].Buyer);
                    buyers.Add(orderList[i].Buyer);
                }
            }

            return orderList;
        }

        public Order? GetOrderByID(int orderid)
        {
            return _context.Orders.
                Include(o => o.Employee).
                Include(o => o.SupplyProducts).
                    ThenInclude(s => s.Product).
                        ThenInclude(s => s.Color).
                Include(o => o.SupplyProducts).
                    ThenInclude(s => s.Product).
                        ThenInclude(s => s.ProductType).
                            ThenInclude(s => s.ProductGroup).
                Include(o => o.SupplyProducts).
                    ThenInclude(s => s.Supply).
                Include(o => o.Buyer).
                    ThenInclude(b => b.Individual).
                Include(o => o.Buyer).
                    ThenInclude(b => b.LegalEntity).
                    Where(o => o.OrderId == orderid).First();
        }

        public void InsertOrder(Order order)
        {
            List<SupplyProduct> supplyProducts = new();

            foreach (SupplyProduct product in order.SupplyProducts) 
            {
                supplyProducts.Add(_context.SupplyProducts.Where(s => s.SupplyProductsId == product.SupplyProductsId).First());
            }
            order.SupplyProducts = supplyProducts;

            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void DeleteOrder(int orderid)
        {
            Order? order = _context.Orders.Find(orderid);

            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        }

        public void UpdateOrder(Order order)
        {
            List<SupplyProduct> supplyProducts = new();

            foreach (SupplyProduct product in order.SupplyProducts)
            {
                supplyProducts.Add(_context.SupplyProducts.Where(s => s.SupplyProductsId == product.SupplyProductsId).First());
            }

            Order editableOrder = _context.Orders.Include(o => o.SupplyProducts).Where(o => o.OrderId == order.OrderId).First();
            editableOrder.SupplyProducts = supplyProducts;

            _context.SaveChanges();
        }
    }
}