namespace BookstoreAPI.Models.DTOs
{
    public class AuthorReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public List<string> BookTitles { get; set; }  // Optionally include related books
    }
}
