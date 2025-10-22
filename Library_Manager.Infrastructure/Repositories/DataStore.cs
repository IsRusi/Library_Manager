using Library_Manager.Application.Interfaces;
using Library_Manager.Application.Interfaces.IRepository;
using Library_Manager.Domain.Models;

namespace Library_Manager.Infrastructure.Repositories
{
    public class DataStore : IDataStore
    {
        public List<Author> Authors { get; } = new();
        public List<Book> Books { get; } = new();

        public IAuthorRepository AuthorRepository { get; }
        public IBookRepository BookRepository { get; }

        public DataStore()
        {
            AuthorRepository = new AuthorRepository(Authors);
            BookRepository = new BookRepository(Books);
        }

    }
}
