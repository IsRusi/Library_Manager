using Library_Manager.Application.Interfaces.IRepository;
using Library_Manager.Domain.Models;
using Library_Manager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Library_Manager.Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryContext _libraryContext;

        public AuthorRepository(LibraryContext context)
        {
            _libraryContext = context;
        }

        public async Task AddAsync(Author author)
        {
            //author.Id = await _libraryContext.Authors.AnyAsync() ? await _libraryContext.Authors.MaxAsync(a => a.Id) + 1 : 0; ;
            await _libraryContext.Authors.AddAsync(author);
            await _libraryContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _libraryContext.Authors
                .Where(author => author.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        => await _libraryContext.Authors.ToListAsync();

        public async Task<Author> GetByIdAsync(int id)
        => await _libraryContext.Authors
            .FirstOrDefaultAsync(author => author.Id == id);

        public async Task UpdateAsync(Author author)
        {
            var existAuthor = await _libraryContext.Authors
                .Where(tempAuthor => tempAuthor.Id == author.Id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(tempAuthor => tempAuthor.Name, author.Name)
                .SetProperty(tempAuthor => tempAuthor.DateOfBirth, author.DateOfBirth));
        }
        public async Task<Author> GetByAuthorIdWithBooksAsync(int id)
            => await _libraryContext.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == id);

        //Получить автора не старше 25
        public async Task<IEnumerable<Author>> GetAuthorsYoungerThanAsync(int authorsAgeLimit)
           => await _libraryContext.Authors
            .Where(author => (DateTime.UtcNow.Year-author.DateOfBirth.Year) <= authorsAgeLimit)
            .ToListAsync();
        public async Task<bool> ExistsAuthorByNameAsync(string authorsName)
            => await _libraryContext.Authors.AnyAsync(author => author.Name == authorsName);
    }
}