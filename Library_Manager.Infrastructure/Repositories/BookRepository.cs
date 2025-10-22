using Library_Manager.Application.Interfaces.IRepository;
using Library_Manager.Domain.Models;

namespace Library_Manager.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly List<Book> _books;

        public BookRepository(DataStore store)
        {
            _books = store.Books;
        }
        public BookRepository(List<Book> books)
        {
            _books = books;
        }

        public Task AddAsync(Book book)
        {
            book.Id = _books.Any() ? _books.Max(a => a.Id) + 1 : 0; ;
            _books.Add(book);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            _books.RemoveAll(book => book.Id == id);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Book>> GetAllAsync()
            => Task.FromResult(_books.AsEnumerable());

        public Task<Book> GetByIdAsync(int id)
            => Task.FromResult(_books.FirstOrDefault(book => book.Id == id));

        public Task UpdateAsync(Book book)
        {
            int index = _books.FindIndex(b => b.Id == book.Id);
            if (index != -1)
            {
                _books[index] = book;
            }
            return Task.CompletedTask;
        }

        public Task<bool> ExistsBookByTitleAsync(string title)
        => Task.FromResult(_books.Any(b => b.Title == title));



    }
}
