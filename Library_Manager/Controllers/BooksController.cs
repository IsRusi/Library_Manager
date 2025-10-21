using Library_Manager.DTO;
using Library_Manager_DAL.Interfaces;
using Library_Manager_DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library_Manager_API.Controllers
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
        public IActionResult GetBooks()
        {

            var books = _bookService.GetAllBooks();
                return Ok(books);

        }

        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            var book = _bookService.GetById(id);
            return Ok(book);
        }

        [HttpPost]
        public IActionResult CreateBook(CreateBookDTO createDto)
        {
            _bookService.AddBook(createDto.Title, createDto.PublishedYear, createDto.AuthorId);
            return Ok("Книга успешно добавлена.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, UpdateBookDTO updateDto)
        {
            var updatedBook = new Book
            {
                Id = id,
                Title = updateDto.Title,
                PublishedYear = updateDto.PublishedYear,
                AuthorId = updateDto.AuthorId
            };

            _bookService.Update(id, updatedBook);
            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeleteBook(int bookId)
        {
            _bookService.DeleteBookById(bookId);
            return NoContent();
        }
    }
}
