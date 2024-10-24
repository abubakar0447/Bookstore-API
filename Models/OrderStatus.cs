namespace BookstoreAPI.Models
{
    public enum OrderStatus
    {
        Pending,
        Shipped,
        Delivered,
        Canceled,
        ReturnRequested = 4,  // New status for return request
        Returned = 5  // New status for returned orders
    }
}
