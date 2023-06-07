using PraktikaAPI.Models;

namespace PraktikaAPI.DAL
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetOrders();
        Order? GetOrderByID(int OrderId);
        void InsertOrder(Order Order);
        void DeleteOrder(int OrderId);
        void UpdateOrder(Order Order);
    }
}
