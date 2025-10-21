namespace Library_Manager_DAL.Interfaces
{
    public interface IDataStore
    {
        IAuthorRepository AuthorRepository { get; }
        IBookRepository BookRepository { get; }
    }
}
