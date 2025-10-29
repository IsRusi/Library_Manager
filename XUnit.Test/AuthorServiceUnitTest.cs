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
    public class AuthorServiceUnitTest
    {
        private Mock<IAuthorRepository> _authorRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private Mock<ILogger<AuthorService>> _loggerMock;
        private AuthorService _authorService;

        public AuthorServiceUnitTest()
        {
            _authorRepositoryMock = new Mock<IAuthorRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<AuthorService>>();
            _authorService = new AuthorService(_authorRepositoryMock.Object, _loggerMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task AddAuthorAsync_FutureDate_ShouldThrowInvalidDateOfBirthException()
        {
            var createDto = new CreateAuthorDTO("the best author in the world", DateTime.Now.AddYears(1));

            _authorRepositoryMock.Setup(r => r.ExistsAuthorByNameAsync(createDto.Name))
                                 .ReturnsAsync(false);

            await Assert.ThrowsAsync<InvalidDateOfBirthException>(() => _authorService.AddAuthorAsync(createDto));

            _authorRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Author>()), Times.Never);
            _mapperMock.Verify(m => m.Map<Author>(It.IsAny<CreateAuthorDTO>()), Times.Never);
        }

        [Fact]
        public async Task AddAuthorAsync_EmptyName_ShouldThrowInvalidNameException()
        {
            var createDto = new CreateAuthorDTO("", DateTime.Now.AddDays(-1));

            await Assert.ThrowsAsync<InvalidNameException>(() => _authorService.AddAuthorAsync(createDto));

            _authorRepositoryMock.Verify(r => r.ExistsAuthorByNameAsync(It.IsAny<string>()), Times.Never);
            _authorRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Author>()), Times.Never);
            _mapperMock.Verify(m => m.Map<Author>(It.IsAny<CreateAuthorDTO>()), Times.Never);
        }

        [Fact]
        public async Task AddAuthorAsync_DuplicateName_ShouldThrowAuthorAlreadyExistsException()
        {
            var createDto = new CreateAuthorDTO("gudini", new DateTime(1828, 9, 9));

            _authorRepositoryMock.Setup(r => r.ExistsAuthorByNameAsync(createDto.Name))
                                 .ReturnsAsync(true);

            await Assert.ThrowsAsync<AuthorAlreadyExistsException>(() => _authorService.AddAuthorAsync(createDto));

            _authorRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Author>()), Times.Never);
            _mapperMock.Verify(m => m.Map<Author>(It.IsAny<CreateAuthorDTO>()), Times.Never);
        }

        [Fact]
        public async Task AddAuthorAsync_ValidData_ShouldAddAuthor()
        {
            var createDto = new CreateAuthorDTO("Leo Tolstoy", new DateTime(1828, 9, 9));
            var author = new Author { Id = 1, Name = createDto.Name, DateOfBirth = createDto.DateOfBirth };
            var authorDto = new AuthorDTO { Id = 1, Name = createDto.Name, DateOfBirth = createDto.DateOfBirth };

            _authorRepositoryMock.Setup(r => r.ExistsAuthorByNameAsync(createDto.Name))
                                 .ReturnsAsync(false);

            _mapperMock.Setup(m => m.Map<Author>(createDto))
                       .Returns(author);

            _mapperMock.Setup(m => m.Map<AuthorDTO>(createDto))
                       .Returns(authorDto);

            var result = await _authorService.AddAuthorAsync(createDto);

            Assert.NotNull(result);
            Assert.Equal(createDto.Name, result.Name);
            _authorRepositoryMock.Verify(r => r.AddAsync(It.Is<Author>(a => a.Name == createDto.Name)), Times.Once);
        }
    }
}