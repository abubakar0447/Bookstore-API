using BookstoreAPI.Models;
using BookstoreAPI.Models.DTOs;
using BookstoreAPI.Repositories.Interfaces;
using BookstoreAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBookRepository _bookRepository;

        public OrderService(IOrderRepository orderRepository, IBookRepository bookRepository)
        {
            _orderRepository = orderRepository;
            _bookRepository = bookRepository;
        }

        public async Task CreateOrderAsync(string customerId, OrderCreateDTO orderCreateDTO)
        {
            var order = new Order
            {
                CustomerId = customerId,
                Status = OrderStatus.Pending,  // Default status
                OrderDetails = new List<OrderDetail>()
            };

            foreach (var item in orderCreateDTO.Items)
            {
                var book = await _bookRepository.GetBookByIdAsync(item.BookId);
                if (book == null)
                {
                    throw new System.Exception($"Book with ID {item.BookId} not found");
                }

                var orderDetail = new OrderDetail
                {
                    BookId = item.BookId,
                    Quantity = item.Quantity,
                    UnitPrice = book.Price,  // Capture the price at the time of order
                    Order = order
                };

                order.OrderDetails.Add(orderDetail);
            }

            await _orderRepository.CreateOrderAsync(order);
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();

            var orderDTOs = orders.Select(o => new OrderDTO
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                Status = o.Status.ToString(),
                CustomerName = $"{o.Customer.FirstName} {o.Customer.LastName}",
                CustomerEmail = o.Customer.Email,
                OrderDetails = o.OrderDetails.Select(od => new OrderDetailDTO2
                {
                    BookId = od.BookId,
                    BookTitle = od.Book.Title,
                    Quantity = od.Quantity,
                    UnitPrice = od.UnitPrice
                }).ToList()
            }).ToList();

            return orderDTOs;
        }

        public async Task<IEnumerable<OrderReadDTO>> GetOrdersByCustomerAsync(string customerId)
        {
            var orders = await _orderRepository.GetOrdersByCustomerAsync(customerId);

            return orders.Select(o => new OrderReadDTO
            {
                OrderId = o.Id,
                OrderDate = o.OrderDate,
                Status = o.Status.ToString(),
                Items = o.OrderDetails.Select(od => new OrderDetailDTO
                {
                    BookTitle = od.Book.Title,
                    Quantity = od.Quantity,
                    UnitPrice = od.UnitPrice
                }).ToList()
            }).ToList();
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _orderRepository.GetOrderByIdAsync(orderId);
        }

        public async Task UpdateOrderStatusAsync(Order order)
        {
            await _orderRepository.UpdateOrderStatusAsync(order);
        }

        // Implement CancelOrderAsync
        public async Task CancelOrderAsync(int orderId, string reason)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                throw new Exception("Order not found");
            }

            if (order.Status != OrderStatus.Pending && order.Status != OrderStatus.Shipped)
            {
                throw new Exception("Cannot cancel order that has been delivered or already canceled.");
                // return Ok(new { Message = "Cannot cancel order that has been delivered or already canceled." });
            }

            order.Status = OrderStatus.Canceled;
            order.CancellationReason = reason;

            // Update the stock quantity back
            foreach (var detail in order.OrderDetails)
            {
                detail.Book.StockQuantity += detail.Quantity;
            }

            await _orderRepository.UpdateOrderStatusAsync(order);
        }

        // Implement RequestReturnAsync
        public async Task RequestReturnAsync(int orderId, string reason)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                throw new Exception("Order not found");
            }

            if (order.Status != OrderStatus.Delivered)
            {
                throw new InvalidOperationException("Cannot request a return for an order that has not been delivered.");
            }

            order.Status = OrderStatus.ReturnRequested;
            order.ReturnReason = reason;

            await _orderRepository.UpdateOrderStatusAsync(order);
        }

        // Implement ApproveReturnAsync
        public async Task ApproveReturnAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                throw new Exception("Order not found");
            }

            if (order.Status != OrderStatus.ReturnRequested)
            {
                throw new InvalidOperationException("Cannot approve return for an order that has not requested a return.");
            }

            order.Status = OrderStatus.Returned;

            // Increment stock quantity back
            foreach (var detail in order.OrderDetails)
            {
                detail.Book.StockQuantity += detail.Quantity;
            }

            await _orderRepository.UpdateOrderStatusAsync(order);
        }
    }
}
