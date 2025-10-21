using Library_Manager_DAL.Models;

namespace Library_Manager_DAL.Interfaces
{
    public interface IAuthorService
    {
        Author GetById(int id);
        IEnumerable<Author> GetAllAuthors();
        void Update(int id,Author entity);
        void DeleteAuthorById(int id);
        Author AddAuthor(string authorName, DateOnly authorDateOfBirth);

    }
}
