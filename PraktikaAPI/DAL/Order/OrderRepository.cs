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
            return _context.Orders.ToList();
        }

        public Order? GetOrderByID(int orderid)
        {
            return _context.Orders.Find(orderid);
        }

        public void InsertOrder(Order order)
        {
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
            _context.Entry(order).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
