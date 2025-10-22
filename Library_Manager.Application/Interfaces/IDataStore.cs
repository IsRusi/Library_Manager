using Library_Manager.Application.Interfaces.IRepository;

namespace Library_Manager.Application.Interfaces
{
    public interface IDataStore
    {
        IAuthorRepository AuthorRepository { get; }
        IBookRepository BookRepository { get; }
    }
}