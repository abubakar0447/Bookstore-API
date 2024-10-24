using Microsoft.AspNetCore.Mvc;
using BookstoreAPI.Services.Interfaces;
using BookstoreAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookstoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // GET: api/authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorReadDTO>>> GetAuthors()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            return Ok(authors);
        }

        // GET: api/authors/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorReadDTO>> GetAuthor(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            if (author == null) return NotFound();

            return Ok(author);
        }

        // POST: api/authors
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddAuthor(AuthorCreateDTO authorCreateDTO)
        {
            await _authorService.AddAuthorAsync(authorCreateDTO);
            return CreatedAtAction(nameof(GetAuthor), new { id = authorCreateDTO.Name }, authorCreateDTO);
        }

        // PUT: api/authors/{id}
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, AuthorCreateDTO authorCreateDTO)
        {
            await _authorService.UpdateAuthorAsync(id, authorCreateDTO);
            return NoContent();
        }

        // DELETE: api/authors/{id}
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            await _authorService.DeleteAuthorAsync(id);
            return NoContent();
        }
    }
}
