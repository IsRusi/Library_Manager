using AutoMapper;
using Library_Manager.Application.DTO;
using Library_Manager.Application.Extensions;
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
        private readonly IMapper _mapper;
        private readonly ILogger<BookService> _logger;

        public BookService(IBookRepository bookRepository, ILogger<BookService> logger, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<BookDTO> AddBookAsync(CreateBookDTO createBookDto)
        {
                await createBookDto.Title.ValidateTitleAsync(this);
                createBookDto.PublishedYear.ValidatePublishedYear();

                var newBook = _mapper.Map<Book>(createBookDto);

                await _bookRepository.AddAsync(newBook);

                var bookDto = _mapper.Map<BookDTO>(newBook);
                _logger.LogInformation($"книга успешно добавлена на id:{bookDto.Id}");
                return bookDto;

        }

        public async Task DeleteBookByIdAsync(int id)
        {
                ValidationExtensionBook.ValidateId(id);
                var bookToDelete = await _bookRepository.GetByIdAsync(id);
                var bookDto = _mapper.Map<BookDTO>(bookToDelete);
                bookDto.ValidateBook();
                await _bookRepository.DeleteAsync(id);
                _logger.LogInformation($"книга успешно удалена по id:{bookDto.Id}");
           
        }

        public async Task<bool> ExistsBookByTitleAsync(string title)
        {
            var book = await _bookRepository.ExistsBookByTitleAsync(title);
            return await Task.FromResult(book);
        }

        public async Task<IEnumerable<BookDTO>> GetAllBooksAsync()
        {
            
          var books = await _bookRepository.GetAllAsync();
          var bookDtos = books.Select(book => _mapper.Map<BookDTO>(book));
          _logger.LogInformation($"успешно получены книги их кол-во:{books.Count()}");
          return bookDtos;
            
        }
        //получить книги по имени автора
        public async Task<IEnumerable<BookWithAuthorDTO>> GetBooksByAuthorsNameAsync(string authorName)
        {
                await authorName.ValidateNameAsync();
                var books = await _bookRepository.GetBooksByAuthorsNameAsync(authorName);
                var bookWithAuthorDto = books.Select(book => _mapper.Map<BookWithAuthorDTO>(book));
                _logger.LogInformation($"книги по имени автора получены их кол-во:{bookWithAuthorDto.Count()}");
                return bookWithAuthorDto;

        }
        //Найти книгу с конкретным автором и за конкретный год
        public async Task<BookWithAuthorDTO> GetBookByAuthorsNameAndYearAsync(string authorName, int year)
        {

                await authorName.ValidateNameAsync();
                year.ValidatePublishedYear();
                var book=await _bookRepository.GetBookByAuthorsNameAndYearAsync(authorName, year);
                var bookDto=_mapper.Map<BookWithAuthorDTO>(book);
                _logger.LogInformation($"Книга найдена по id:{bookDto.Id}. Автор: {authorName}, Год: {year}, Заголовок: {bookDto.Title}");
                return bookDto;
            
        }

        public async Task<BookDTO> GetByIdAsync(int id)
        {

                ValidationExtensionBook.ValidateId(id);
                var book = await _bookRepository.GetByIdAsync(id);
                var bookDto = _mapper.Map<BookDTO>(book);
                bookDto.ValidateBook();
                _logger.LogInformation($"книга найдена по Id: {id}, Заголовок: {bookDto.Title}");

            return bookDto;
            
        }

        public async Task UpdateAsync(int id, UpdateBookDTO updateBook)
        {
           
                var bookToUpdate = await _bookRepository.GetByIdAsync(id);
                var bookDto = _mapper.Map<BookDTO>(bookToUpdate);
                bookDto.ValidateBook();

                await bookDto.Title.ValidateTitleAsync(this);
                bookDto.PublishedYear.ValidatePublishedYear();

                _mapper.Map(bookDto,bookToUpdate);

                await _bookRepository.UpdateAsync(bookToUpdate);
                _logger.LogInformation($"изменение завершёно. Id: {id}, Заголовок: {updateBook.Title}");
        }
    }
}