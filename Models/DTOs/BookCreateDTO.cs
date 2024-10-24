using System.ComponentModel.DataAnnotations;

namespace BookstoreAPI.Models.DTOs
{
    public class BookCreateDTO
    {
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(100, ErrorMessage = "Title can't be longer than 100 characters")]
        public string Title { get; set; }


        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 10000, ErrorMessage = "Price must be between 0.01 and 10,000")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "ISBN is required")]
        [MaxLength(13, ErrorMessage = "ISBN can't be longer than 13 characters")]
        public string ISBN { get; set; }
        
        [Required(ErrorMessage = "Stock quantity is required")]
        public int StockQuantity { get; set; }

        [Required(ErrorMessage = "Author is required")]
        public int AuthorId { get; set; }
    }
}
