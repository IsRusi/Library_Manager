using Library_Manager.Application.Interfaces;
using Library_Manager.Application.Interfaces.IRepository;
using Library_Manager.Infrastructure.Data;

namespace Library_Manager.Infrastructure.Repositories
{
    public class DataStore : IDataStore
    {
        private readonly LibraryContext _libraryContext;

        public DataStore(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        public IAuthorRepository AuthorRepository { get; }
        public IBookRepository BookRepository { get; }
    }
}