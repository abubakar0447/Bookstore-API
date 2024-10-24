using BookstoreAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookstoreAPI.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task CreateOrderAsync(Order order);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<IEnumerable<Order>> GetOrdersByCustomerAsync(string customerId);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task UpdateOrderStatusAsync(Order order);
    }
}
