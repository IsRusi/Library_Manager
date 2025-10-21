using Library_Manager_DAL.Models;

namespace Library_Manager_DAL.Interfaces
{
    public interface IBookService
    {
        void AddBook(string title, int publishedYear, int authorId);
        void DeleteBookById(int id);
        IEnumerable<Book> GetAllBooks();

        Book GetById(int id);
    
        void Update(int id, Book updatedBook);

    }
}
