using BookstoreAPI.Models;
using BookstoreAPI.Repositories.Interfaces;
using BookstoreAPI.Services.Interfaces;
using BookstoreAPI.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace BookstoreAPI.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<IEnumerable<AuthorReadDTO>> GetAllAuthorsAsync()
        {
            var authors = await _authorRepository.GetAllAuthorsAsync();
            return authors.Select(a => new AuthorReadDTO
            {
                Id = a.Id,
                Name = a.Name,
                Bio = a.Bio,
                BookTitles = a.Books.Select(b => b.Title).ToList()
            }).ToList();
        }

        public async Task<AuthorReadDTO> GetAuthorByIdAsync(int id)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(id);
            if (author == null) return null;

            return new AuthorReadDTO
            {
                Id = author.Id,
                Name = author.Name,
                Bio = author.Bio,
                BookTitles = author.Books.Select(b => b.Title).ToList()
            };
        }

        public async Task AddAuthorAsync(AuthorCreateDTO authorCreateDTO)
        {
            var author = new Author
            {
                Name = authorCreateDTO.Name,
                Bio = authorCreateDTO.Bio
            };
            await _authorRepository.AddAuthorAsync(author);
        }

        public async Task UpdateAuthorAsync(int id, AuthorCreateDTO authorCreateDTO)
        {
            var existingAuthor = await _authorRepository.GetAuthorByIdAsync(id);
            if (existingAuthor != null)
            {
                existingAuthor.Name = authorCreateDTO.Name;
                existingAuthor.Bio = authorCreateDTO.Bio;
                await _authorRepository.UpdateAuthorAsync(existingAuthor);
            }
        }

        public async Task DeleteAuthorAsync(int id)
        {
            await _authorRepository.DeleteAuthorAsync(id);
        }
    }
}
