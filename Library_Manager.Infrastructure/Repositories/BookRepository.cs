using Library_Manager.Application.Interfaces.IRepository;
using Library_Manager.Domain.Models;
using Library_Manager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Library_Manager.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryContext _libraryContext;

        public BookRepository(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        public async Task AddAsync(Book book)
        {
            await _libraryContext.Books.AddAsync(book);
            await _libraryContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _libraryContext.Books
                .Where(book => book.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
            => await _libraryContext.Books.ToListAsync();

        public async Task<Book> GetByIdAsync(int id)
            => await _libraryContext.Books.FirstOrDefaultAsync(book => book.Id == id);

        public async Task UpdateAsync(Book book)
        {
            await _libraryContext.Books.Where(updateBook => updateBook.Id == book.Id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(updateBook => updateBook.Title, book.Title)
                .SetProperty(updateBook => updateBook.PublishedYear, book.PublishedYear)
                .SetProperty(updateBook => updateBook.AuthorId, book.AuthorId)
                );
        }

        //получить книги по имени автора
        public async Task<IEnumerable<Book>> GetBooksByAuthorsNameAsync(string authorName)
           => await _libraryContext.Books
                .Include(book => book.Author)
                .Where(book=>book.Author.Name.StartsWith(authorName))
                .ToListAsync();
        //Найти книгу с конкретным автором и за конкретный год
        public async Task<Book> GetBookByAuthorsNameAndYearAsync(string authorName, int year)
        => await _libraryContext.Books
            .Include(book => book.Author)
            .FirstOrDefaultAsync(book=>book.Author.Name.StartsWith(authorName) && book.PublishedYear.Equals(year));

        public Task<bool> ExistsBookByTitleAsync(string title)
        => Task.FromResult(_libraryContext.Books.Any(b => b.Title == title));
    }
}