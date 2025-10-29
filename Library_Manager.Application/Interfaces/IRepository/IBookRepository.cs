using Library_Manager.Domain.Models;

namespace Library_Manager.Application.Interfaces.IRepository
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<bool> ExistsBookByTitleAsync(string title);
        Task<IEnumerable<Book>> GetBooksByAuthorsNameAsync(string authorName);
        Task<Book> GetBookByAuthorsNameAndYearAsync(string authorName, int year);
    }
}