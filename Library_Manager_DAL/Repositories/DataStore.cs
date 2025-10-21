using Library_Manager_DAL.Interfaces;
using Library_Manager_DAL.Models;

namespace Library_Manager_DAL.Repositories
{
    public class DataStore : IDataStore
    {
        public List<Author> Authors { get; } = new();
        public List<Book> Books { get; }= new();

        public IAuthorRepository AuthorRepository { get; }
        public IBookRepository BookRepository { get; }

        public DataStore()
        {
            AuthorRepository = new AuthorRepository(Authors);
            BookRepository = new BookRepository(Books);
        }

    }
}
