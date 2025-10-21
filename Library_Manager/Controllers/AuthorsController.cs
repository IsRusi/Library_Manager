using Library_Manager.DTO;
using Library_Manager_DAL.Interfaces;
using Library_Manager_DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library_Manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController: ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public IActionResult GetAuthors()
        {
                var authors = _authorService.GetAllAuthors();
                return Ok(authors);
            
        }

        [HttpGet("{id}")]
        public IActionResult GetAuthor(int id)
        {
            var author = _authorService.GetById(id);
            return Ok(author);
        }

        [HttpPost]
        public IActionResult CreateAuthor(CreateAuthorDTO createdAuthorDTO)
        {
            var createdAuthor = _authorService.AddAuthor(createdAuthorDTO.Name, createdAuthorDTO.DateOfBirth);
            return Ok("Автор успешно добавлен");
            
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, UpdateAuthorDto updateAuthorDTO)
        {
            _authorService.Update(id, new Author
            {
                Id = id,
                Name = updateAuthorDTO.Name,
                DateOfBirth = updateAuthorDTO.DateOfBirth
            });

            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeleteAuthor(int authorId)
        {
            _authorService.DeleteAuthorById(authorId);
            return NoContent();
        }
    }
}
