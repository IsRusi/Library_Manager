using Library_Manager.Application.DTO;
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
        public AuthorService(IAuthorRepository authorRepository, ILogger<AuthorService> logger)
        {
            _authorRepository = authorRepository;
            _logger = logger;
        }

        public async Task<AuthorDTO> AddAuthorAsync(CreateAuthorDTO createAuthorDTO)
        {
            try
            {

                await createAuthorDTO.Name.ValidateNameAsync(this);
                createAuthorDTO.DateOfBirth.ValidateDate();


                var author = new Author()
                {
                    Name = createAuthorDTO.Name,
                    DateOfBirth = createAuthorDTO.DateOfBirth
                };
                var authorDto = new AuthorDTO()
                {
                    Name = createAuthorDTO.Name,
                    DateOfBirth = createAuthorDTO.DateOfBirth
                };
                await _authorRepository.AddAsync(author);

                return await Task.FromResult(authorDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new AuthorDTO();
            }
        }

        public async Task DeleteAuthorByIdAsync(int id)
        {
            try
            {
                id.ValidateId();
                var authorToDelete = await _authorRepository.GetByIdAsync(id);
                var authorDto = new AuthorDTO()
                {
                    Id = authorToDelete.Id,
                    Name = authorToDelete.Name,
                    DateOfBirth = authorToDelete.DateOfBirth
                };
                authorDto.ValidateAuthor();
                await _authorRepository.DeleteAsync(id);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

            }
        }

        public async Task<bool> ExistsAuthorByNameAsync(string authorName)
        {
            var author = await _authorRepository.ExistsAuthorByNameAsync(authorName);
            return await Task.FromResult(author);
        }

        public async Task<IEnumerable<AuthorDTO>> GetAllAuthorsAsync()
        {
            var authors = await _authorRepository.GetAllAsync();

            var authorsDtos = authors.Select(author => new AuthorDTO(
                author.Id,
                author.Name,
                author.DateOfBirth
            ));
            return authorsDtos;
        }


        public async Task<AuthorDTO> GetByIdAsync(int id)
        {
            try
            {
                id.ValidateId();
                var author = await _authorRepository.GetByIdAsync(id);
                var authorDto = new AuthorDTO(
                        author.Id,
                        author.Name,
                        author.DateOfBirth);
                authorDto.ValidateAuthor();
                return await Task.FromResult(authorDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new AuthorDTO();
            }
        }

        public async Task UpdateAsync(int id, UpdateAuthorDTO updateAuthor)
        {
            try
            {
                var authorToUpdate = await _authorRepository.GetByIdAsync(id);
                var authorDto = new AuthorDTO() { Id = authorToUpdate.Id, Name = authorToUpdate.Name, DateOfBirth = authorToUpdate.DateOfBirth };
                authorDto.ValidateAuthor();

                await updateAuthor.Name.ValidateNameAsync(this);
                updateAuthor.DateOfBirth.ValidateDate();

                authorToUpdate.Name = updateAuthor.Name;
                authorToUpdate.DateOfBirth = updateAuthor.DateOfBirth;

                await _authorRepository.UpdateAsync(authorToUpdate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                
            }
        }


    }
}
