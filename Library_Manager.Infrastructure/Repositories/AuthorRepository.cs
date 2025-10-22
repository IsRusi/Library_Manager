using Library_Manager.Application.Interfaces.IRepository;
using Library_Manager.Domain.Models;


namespace Library_Manager.Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly List<Author> _authors;

        public AuthorRepository(DataStore store)
        {
            _authors = store.Authors;

        }
        public AuthorRepository(List<Author> authors)
        {
            _authors = authors;

        }

        public Task AddAsync(Author author)
        {
            author.Id = _authors.Any() ? _authors.Max(a => a.Id) + 1 : 0; ;
            _authors.Add(author);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            int affectedRows = _authors.RemoveAll(author => author.Id == id);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Author>> GetAllAsync()
        => Task.FromResult(_authors.AsEnumerable());

        public async Task<Author> GetByIdAsync(int id)
        => await Task.FromResult(_authors.FirstOrDefault(author => author.Id == id));

        public Task UpdateAsync(Author author)
        {
            int authorId = _authors.FindIndex(authorItem => authorItem.Id == author.Id);
            if (authorId != -1)
            {
                _authors[authorId] = author;
            }
            return Task.CompletedTask;
        }
        public Task<bool> ExistsAuthorByNameAsync(string name)
        {
            bool exists = _authors.Any(author => author.Name == name);
            return Task.FromResult(exists);
        }

    }
}
