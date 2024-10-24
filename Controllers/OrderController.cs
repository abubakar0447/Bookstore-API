using Microsoft.AspNetCore.Mvc;
using BookstoreAPI.Services.Interfaces;
using BookstoreAPI.Models.DTOs;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace BookstoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]  // Require authorization for all order-related actions
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // POST: api/order (Place a new order)
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDTO orderCreateDTO)
        {
            // Get the customer ID from the token
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(customerId))
            {
                return Unauthorized(new { Message = "User ID not found in token" });
            }

            // Since customerId is a string (GUID), pass it as is
            await _orderService.CreateOrderAsync(customerId, orderCreateDTO);
            return Ok(new { Message = "Order placed successfully" });
        }

        [HttpGet("GetAllOrders")]
        [Authorize(Roles = "Admin")]  // Only Admins can access this endpoint
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        // GET: api/order (Get order history for the logged-in user)
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(customerId))
            {
                return Unauthorized(new { Message = "User ID not found in token" });
            }

            var orders = await _orderService.GetOrdersByCustomerAsync(customerId);
            return Ok(orders);
        }
        
        [HttpPatch("{orderId}/status")]
        [Authorize(Roles = "Admin")]  // Only admins can update the order status
        public async Task<IActionResult> UpdateOrderStatus(int orderId, OrderStatusUpdateDTO orderStatusUpdateDTO)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return NotFound(new { Message = "Order not found" });
            }

            // Update the order status
            order.Status = orderStatusUpdateDTO.Status;
            await _orderService.UpdateOrderStatusAsync(order);

            return Ok(new { Message = $"Order status updated to {order.Status}" });
        }

        // PATCH: api/order/{orderId}/cancel
        [HttpPatch("{orderId}/cancel")]
        public async Task<IActionResult> CancelOrder(int orderId, [FromBody] string reason)
        {
            await _orderService.CancelOrderAsync(orderId, reason);
            return Ok(new { Message = "Order canceled successfully" });
        }

        // PATCH: api/order/{orderId}/requestReturn
        [HttpPatch("{orderId}/requestReturn")]
        public async Task<IActionResult> RequestReturn(int orderId, [FromBody] string reason)
        {
            await _orderService.RequestReturnAsync(orderId, reason);
            return Ok(new { Message = "Return requested successfully" });
        }

        // PATCH: api/order/{orderId}/approveReturn
        [HttpPatch("{orderId}/approveReturn")]
        [Authorize(Roles = "Admin")]  // Only admins can approve returns
        public async Task<IActionResult> ApproveReturn(int orderId)
        {
            await _orderService.ApproveReturnAsync(orderId);
            return Ok(new { Message = "Return approved successfully" });
        }
    }
}
