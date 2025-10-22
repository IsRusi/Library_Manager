using Library_Manager.Application.DTO;
using Library_Manager.Application.Interfaces.IService;
using Microsoft.AspNetCore.Mvc;

namespace Library_Manager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }


        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            return Ok(book);
        }


        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookDTO createDto)
        {
            var createdBook = await _bookService.AddBookAsync(createDto);
            return CreatedAtAction(nameof(GetBook), new { id = createdBook.Id }, createdBook);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookDTO updateDto)
        {
            var updateBookDto = new UpdateBookDTO
            {
                Title = updateDto.Title,
                PublishedYear = updateDto.PublishedYear,
                AuthorId = updateDto.AuthorId
            };

            await _bookService.UpdateAsync(id, updateBookDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _bookService.DeleteBookByIdAsync(id);
            return NoContent();
        }
    }
}