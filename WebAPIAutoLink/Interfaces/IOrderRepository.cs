using WebAPIAutoLink.DTO;
using WebAPIAutoLink.Models;

namespace WebAPIAutoLink.Interfaces
{
    public interface IOrderRepository
    {
        ICollection<Order> GetOrders();
        Order GetOrder(int id);
        ICollection<Order> GetUserOrders(int id);
        ICollection<Order> GetNotConfirmedOrder();
        bool OrderExists(int id);
        bool CreateOrder(Order order);
        bool UpdateOrder(Order order);
        bool DeleteOrder(Order order);
        bool Save();
    }
}
