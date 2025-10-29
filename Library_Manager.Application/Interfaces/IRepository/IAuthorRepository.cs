using Library_Manager.Domain.Models;

namespace Library_Manager.Application.Interfaces.IRepository
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<bool> ExistsAuthorByNameAsync(string name);
        Task<IEnumerable<Author>> GetAuthorsYoungerThanAsync(int authorsAgeLimit);
        Task<Author> GetByAuthorIdWithBooksAsync(int id);
    }
}