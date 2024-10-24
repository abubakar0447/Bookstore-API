using BookstoreAPI.Models;
using BookstoreAPI.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookstoreAPI.Services.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrderAsync(string customerId, OrderCreateDTO orderCreateDTO);
        Task<IEnumerable<OrderDTO>> GetAllOrdersAsync();
        Task<IEnumerable<OrderReadDTO>> GetOrdersByCustomerAsync(string customerId);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task UpdateOrderStatusAsync(Order order);
        Task CancelOrderAsync(int orderId, string reason);  // New method for order cancellation
        Task RequestReturnAsync(int orderId, string reason);  // New method for requesting a return
        Task ApproveReturnAsync(int orderId);  // New method for approving a return
    }
}
