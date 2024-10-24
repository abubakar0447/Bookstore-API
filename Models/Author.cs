namespace BookstoreAPI.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }

        // Navigation property
        public ICollection<Book> Books { get; set; }
    }
}
