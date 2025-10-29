using AutoMapper;
using Library_Manager.Application.DTO;
using Library_Manager.Application.Exceptions;
using Library_Manager.Application.Interfaces.IRepository;
using Library_Manager.Application.Service;
using Library_Manager.Domain.Models;
using Microsoft.Extensions.Logging;
using Moq;

namespace XUnit.Test
{
    public class BookServiceUnitTest
    {
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<BookService>> _loggerMock;
        private readonly BookService _bookService;

        public BookServiceUnitTest()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<BookService>>();

            _bookService = new BookService(
                _bookRepositoryMock.Object,
                _loggerMock.Object,
                _mapperMock.Object);
        }

        [Fact]
        public async Task AddBookAsync_TitleIsEmpty_ShouldThrowInvalidTitleException()
        {
            var createDto = new CreateBookDTO("", 2000, 1);

            await Assert.ThrowsAsync<InvalidTitleException>(() => _bookService.AddBookAsync(createDto));

            _bookRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Book>()), Times.Never);
            _mapperMock.Verify(m => m.Map<Book>(It.IsAny<CreateBookDTO>()), Times.Never);
        }

        [Fact]
        public async Task AddBookAsync_PublishedYearInFuture_ShouldThrowInvalidPublishedYearException()
        {
            var createDto = new CreateBookDTO("ratatouille in the future", DateTime.Now.Year + 1, 1);

            _bookRepositoryMock.Setup(r => r.ExistsBookByTitleAsync(createDto.Title))
                               .ReturnsAsync(false);

            await Assert.ThrowsAsync<InvalidPublishedYearException>(() => _bookService.AddBookAsync(createDto));

            _bookRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Book>()), Times.Never);
            _mapperMock.Verify(m => m.Map<Book>(It.IsAny<CreateBookDTO>()), Times.Never);
        }

        [Fact]
        public async Task AddBookAsync_TitleAlreadyExists_ShouldThrowBookAlreadyExistsException()
        {
            var createDto = new CreateBookDTO("spagetti and turtle", 1869, 1);

            _bookRepositoryMock.Setup(r => r.ExistsBookByTitleAsync(createDto.Title))
                               .ReturnsAsync(true);

            await Assert.ThrowsAsync<BookAlreadyExistsException>(() => _bookService.AddBookAsync(createDto));

            _bookRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Book>()), Times.Never);
            _mapperMock.Verify(m => m.Map<Book>(It.IsAny<CreateBookDTO>()), Times.Never);
        }

        [Fact]
        public async Task AddBookAsync_ValidData_ShouldAddBook()
        {
            var createDto = new CreateBookDTO("The Great Gatsby", 1925, 1);
            var book = new Book { Id = 1, Title = createDto.Title, PublishedYear = createDto.PublishedYear, AuthorId = createDto.AuthorId };
            var bookDto = new BookDTO { Id = 1, Title = createDto.Title, PublishedYear = createDto.PublishedYear, AuthorId = createDto.AuthorId };

            _bookRepositoryMock.Setup(r => r.ExistsBookByTitleAsync(createDto.Title))
                               .ReturnsAsync(false);

            _mapperMock.Setup(m => m.Map<Book>(createDto))
                       .Returns(book);

            _mapperMock.Setup(m => m.Map<BookDTO>(book))
                       .Returns(bookDto);

            var result = await _bookService.AddBookAsync(createDto);

            Assert.NotNull(result);
            Assert.Equal(createDto.Title, result.Title);
            _bookRepositoryMock.Verify(r => r.AddAsync(It.Is<Book>(b => b.Title == createDto.Title)), Times.Once);
        }


    }
}