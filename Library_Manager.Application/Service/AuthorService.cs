using AutoMapper;
using Library_Manager.Application.DTO;
using Library_Manager.Application.Extensions;
using Library_Manager.Application.Interfaces.IRepository;
using Library_Manager.Application.Interfaces.IService;
using Library_Manager.Domain.Models;
using Microsoft.Extensions.Logging;
using static Library_Manager.Application.Extensions.ValidationExtensionAuthor;

namespace Library_Manager.Application.Service
{
    public class AuthorService : IAuthorService
    {
        private readonly ILogger<AuthorService> _logger;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository authorRepository, ILogger<AuthorService> logger, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<AuthorDTO> AddAuthorAsync(CreateAuthorDTO createAuthorDTO)
        {
            await createAuthorDTO.Name.ValidateNameAsync(this);
            createAuthorDTO.DateOfBirth.ValidateDate();

            var author = _mapper.Map<Author>(createAuthorDTO);
            var authorDto = _mapper.Map<AuthorDTO>(createAuthorDTO);

            await _authorRepository.AddAsync(author);
            _logger.LogInformation($"автор {author.Name} добавлен. его id: {author.Id})");

            return authorDto;
        }

        public async Task DeleteAuthorByIdAsync(int id)
        {

                ValidationExtensionAuthor.ValidateId(id);
                var authorToDelete = await _authorRepository.GetByAuthorIdWithBooksAsync(id);
                authorToDelete.ValidateAuthorsBooks();
                var authorDto = _mapper.Map<AuthorDTO>(authorToDelete);
                authorDto.ValidateAuthor();

                await _authorRepository.DeleteAsync(id);
                _logger.LogInformation($"автор удалён. Id: {id}");
        }

        public async Task<IEnumerable<AuthorDTO>> GetAuthorsYoungerThanAsync(int authorsAgeLimit)
        {

                authorsAgeLimit.ValidateAge();
                var authors = await _authorRepository.GetAuthorsYoungerThanAsync(authorsAgeLimit);
                var authorsDto = authors.Select(author => _mapper.Map<AuthorDTO>(author));
                _logger.LogInformation($"метод успешно выполнен количество полученных авторов: {authorsDto.Count()}");
                return authorsDto;
        }

        public async Task<bool> ExistsAuthorByNameAsync(string authorName)
        {
            var author = await _authorRepository.ExistsAuthorByNameAsync(authorName);
            return await Task.FromResult(author);
        }

        public async Task<IEnumerable<AuthorDTO>> GetAllAuthorsAsync()
        {
            var authors = await _authorRepository.GetAllAsync();

            var authorsDtos = authors.Select(author => _mapper.Map<AuthorDTO>(author));
            return authorsDtos;
        }

        public async Task<AuthorDTO> GetByIdAsync(int id)
        {

                ValidationExtensionAuthor.ValidateId(id);
                var author = await _authorRepository.GetByIdAsync(id);
                var authorDto = _mapper.Map<AuthorDTO>(author);
                authorDto.ValidateAuthor();
                _logger.LogInformation($"автор {author.Name} получен с id:{id}");
                return await Task.FromResult(authorDto);
                
        }

        public async Task UpdateAsync(int id, UpdateAuthorDTO updateAuthor)
        {
                var authorToUpdate = await _authorRepository.GetByIdAsync(id);
                var authorDto = _mapper.Map<AuthorDTO>(authorToUpdate);
                authorDto.ValidateAuthor();

                await updateAuthor.Name.ValidateNameAsync(this);
                updateAuthor.DateOfBirth.ValidateDate();

                _mapper.Map(updateAuthor, authorToUpdate);
                _logger.LogInformation($"автор изменён id:{id}");
                await _authorRepository.UpdateAsync(authorToUpdate);
        }
    }
}