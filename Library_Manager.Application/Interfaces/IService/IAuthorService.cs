using Library_Manager.Application.DTO;

namespace Library_Manager.Application.Interfaces.IService
{
    public interface IAuthorService
    {
        Task<AuthorDTO> GetByIdAsync(int id);

        Task<IEnumerable<AuthorDTO>> GetAllAuthorsAsync();

        Task UpdateAsync(int id, UpdateAuthorDTO entity);

        Task DeleteAuthorByIdAsync(int id);

        Task<AuthorDTO> AddAuthorAsync(CreateAuthorDTO createAuthorDTO);

        Task<bool> ExistsAuthorByNameAsync(string authorName);
        Task<IEnumerable<AuthorDTO>> GetAuthorsYoungerThanAsync(int authorsAgeLimit);
    }
}