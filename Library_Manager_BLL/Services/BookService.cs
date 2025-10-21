using Library_Manager_DAL.Interfaces;
using Library_Manager_DAL.Models;
using Microsoft.Extensions.Logging;

namespace Library_Manager_BLL.Services
{
    public class BookService:IBookService
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

        public void AddBook(string title, int publishedYear, int authorId)
        {
            try {
                ValidateTitle(title);
                ValidatePublishedYear(publishedYear);
                ValidateAuthorExists(authorId);

                var newBook = new Book
                {
                    Title = title,
                    PublishedYear = publishedYear,
                    AuthorId = authorId
                };

                _bookRepository.Add(newBook);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public void DeleteBookById(int id)
        {
            try {
                ValidateId(id);
                var bookToDelete = _bookRepository.GetById(id);
                ValidateBook(bookToDelete);
                _bookRepository.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<Book> GetAllBooks()
            => _bookRepository.GetAll();



        public Book GetById(int id)
        {
            try {
                ValidateId(id);
                var book = _bookRepository.GetById(id);
                ValidateBook(book);
                return book;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public void Update(int id, Book updatedBook)
        {
            try {
                var bookToUpdate = _bookRepository.GetById(id);
                ValidateBook(bookToUpdate);

                ValidateTitle(updatedBook.Title);
                ValidatePublishedYear(updatedBook.PublishedYear);
                ValidateAuthorExists(updatedBook.AuthorId);

                bookToUpdate.Title = updatedBook.Title;
                bookToUpdate.PublishedYear = updatedBook.PublishedYear;
                bookToUpdate.AuthorId = updatedBook.AuthorId;

                _bookRepository.Update(bookToUpdate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        private void ValidateBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException($"Книга с таким id {book?.Id} не существует");
        }

        private void ValidateId(int id)
        {
            if (id < 0)
                throw new ArgumentException("Id не может быть отрицательным");
        }

        private void ValidateTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Название книги не может быть пустым");

            if (_bookRepository.ExistsBookByTitle(title))
                throw new ArgumentException("Книга с таким названием уже существует");
        }

        private void ValidatePublishedYear(int year)
        {
            if (year < 0 || year > DateTime.UtcNow.Year)
                throw new ArgumentException("Год публикации указан некорректно");
        }

        private void ValidateAuthorExists(int authorId)
        {
            var author = _authorRepository.GetById(authorId);
            if (author == null)
                throw new ArgumentException($"Автор с id={authorId} не найден");
        }
    }
}
