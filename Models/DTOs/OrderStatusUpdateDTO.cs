using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BookstoreAPI.Models.DTOs
{
    public class OrderStatusUpdateDTO
    {
        [JsonConverter(typeof(StringEnumConverter))] // Add this to ensure enum is parsed from string
        public OrderStatus Status { get; set; }
    }
}
