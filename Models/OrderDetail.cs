namespace BookstoreAPI.Models
{
    public class OrderDetail
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }  // The price of the book when the order was placed
    }
}
