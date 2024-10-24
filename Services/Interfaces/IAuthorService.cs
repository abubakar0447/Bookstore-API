using BookstoreAPI.Models;
using BookstoreAPI.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookstoreAPI.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorReadDTO>> GetAllAuthorsAsync();
        Task<AuthorReadDTO> GetAuthorByIdAsync(int id);
        Task AddAuthorAsync(AuthorCreateDTO authorCreateDTO);
        Task UpdateAuthorAsync(int id, AuthorCreateDTO authorCreateDTO);
        Task DeleteAuthorAsync(int id);
    }
}
