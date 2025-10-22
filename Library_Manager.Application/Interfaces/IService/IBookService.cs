using Library_Manager.Domain.Models;
using Library_Manager.Application.DTO;


namespace Library_Manager.Application.Interfaces.IService
{
    public interface IBookService
    {
        Task<BookDTO> AddBookAsync(CreateBookDTO createBookDto);
        Task DeleteBookByIdAsync(int id);
        Task<IEnumerable<BookDTO>> GetAllBooksAsync();

        Task<BookDTO> GetByIdAsync(int id);
    
        Task UpdateAsync(int id, UpdateBookDTO updatedBook);
        Task<bool> ExistsBookByTitleAsync(string title);
    }
}
