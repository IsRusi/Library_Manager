using Library_Manager.Application.DTO;
using Library_Manager.Application.Interfaces.IService;
using Microsoft.AspNetCore.Mvc;

namespace Library_Manager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            var author = await _authorService.GetByIdAsync(id);
            return Ok(author);
        }
        //        Найти книгу с конкретным автором и за конкретную дату.
        //Получить книги автора.
        //Найти автора с возрастом не старше 25;
        [HttpGet("by-age")]
        public async Task<IActionResult> GetAuthorsYoungerThanAsync(int authorAgeLimit)
        {
            var author = await _authorService.GetAuthorsYoungerThanAsync(authorAgeLimit);
            return Ok(author);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorDTO createdAuthorDTO)
        {
            var created = await _authorService.AddAuthorAsync(createdAuthorDTO);
            return CreatedAtAction(nameof(GetAuthor), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] UpdateAuthorDTO inputUpdateAuthorDTO)
        {
            var updateAuthorDTO = new UpdateAuthorDTO
            {
                Name = inputUpdateAuthorDTO.Name,
                DateOfBirth = inputUpdateAuthorDTO.DateOfBirth
            };

            await _authorService.UpdateAsync(id, updateAuthorDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            await _authorService.DeleteAuthorByIdAsync(id);
            return NoContent();
        }
    }
}