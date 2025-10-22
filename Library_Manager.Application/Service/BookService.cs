using Library_Manager.Application.DTO;
using Library_Manager.Application.Interfaces.IRepository;
using Library_Manager.Application.Interfaces.IService;
using Library_Manager.Domain.Models;
using Microsoft.Extensions.Logging;
using static Library_Manager.Application.Extensions.ValidationExtensionBook;


namespace Library_Manager.Application.Service
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ILogger<AuthorService> _logger;
        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository, ILogger<AuthorService> logger)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _logger = logger;
        }

        public async Task<BookDTO> AddBookAsync(CreateBookDTO createBookDto)
        {
            try
            {
                await createBookDto.Title.ValidateTitleAsync(this);
                createBookDto.PublishedYear.ValidatePublishedYear();


                var newBook = new Book
                {
                    Title = createBookDto.Title,
                    PublishedYear = createBookDto.PublishedYear,
                    AuthorId = createBookDto.AuthorId
                };

                await _bookRepository.AddAsync(newBook);
                var bookDto = new BookDTO()
                {
                    Id = newBook.Id,
                    Title = newBook.Title,
                    PublishedYear = newBook.PublishedYear,
                    AuthorId = newBook.AuthorId
                };
                return bookDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new BookDTO();
            }

        }

        public async Task DeleteBookByIdAsync(int id)
        {
            try
            {
                id.ValidateId();
                var bookToDelete = await _bookRepository.GetByIdAsync(id);
                var bookDto = new BookDTO(bookToDelete.Id, bookToDelete.Title, bookToDelete.PublishedYear, bookToDelete.AuthorId);
                bookDto.ValidateBook();
                await _bookRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        public async Task<bool> ExistsBookByTitleAsync(string title)
        {
            var book = await _bookRepository.ExistsBookByTitleAsync(title);
            return await Task.FromResult(book);
        }

        public async Task<IEnumerable<BookDTO>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllAsync();

            var bookDtos = books.Select(book => new BookDTO(
                book.Id,
                book.Title,
                book.PublishedYear,
                book.AuthorId
            ));
            return bookDtos;

        }



        public async Task<BookDTO> GetByIdAsync(int id)
        {
            try
            {
                id.ValidateId();
                var book = await _bookRepository.GetByIdAsync(id);
                var bookDto = new BookDTO(book.Id, book.Title, book.PublishedYear, book.AuthorId);
                bookDto.ValidateBook();
                return bookDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new BookDTO();
            }
        }

        public async Task UpdateAsync(int id, UpdateBookDTO updateBook)
        {
            try
            {
                var bookToUpdate = await _bookRepository.GetByIdAsync(id);
                var bookDto = new BookDTO(bookToUpdate.Id, bookToUpdate.Title, bookToUpdate.PublishedYear, bookToUpdate.AuthorId);
                bookDto.ValidateBook();

                await bookDto.Title.ValidateTitleAsync(this);
                bookDto.PublishedYear.ValidatePublishedYear();

                bookToUpdate.Title = bookDto.Title;
                bookToUpdate.PublishedYear = bookDto.PublishedYear;
                bookToUpdate.AuthorId = bookDto.AuthorId;

                await _bookRepository.UpdateAsync(bookToUpdate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }


    }
}
