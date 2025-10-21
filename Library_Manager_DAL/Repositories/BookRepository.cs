using Library_Manager_DAL.Interfaces;
using Library_Manager_DAL.Models;

namespace Library_Manager_DAL.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly List<Book> _books;

        public BookRepository(DataStore store )
        {
            _books= store.Books;
        }
        public BookRepository(List<Book> books)
        {
            _books = books;
        }

        public void Add(Book book)
        {
            book.Id = _books.Any() ? _books.Max(a => a.Id) + 1 : 1; ;
            _books.Add(book);
        }

        public void Delete(int id)
        {
            _books.RemoveAll(book => book.Id == id);
        }

        public IEnumerable<Book> GetAll()
            => _books;

        public Book GetById(int id)
            => _books.FirstOrDefault(book => book.Id == id);

        public void Update(Book book)
        {
            int index = _books.FindIndex(b => b.Id == book.Id);
            if (index != -1)
            {
                _books[index] = book;
            }
        }

        public bool ExistsBookByTitle(string title)
        => _books.Any(b => b.Title == title);


        
    }
}
