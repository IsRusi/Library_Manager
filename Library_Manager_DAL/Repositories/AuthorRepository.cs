using Library_Manager_DAL.Interfaces;
using Library_Manager_DAL.Models;
using System;

namespace Library_Manager_DAL.Repositories
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

        public void Add(Author author)
        {
            author.Id = _authors.Any() ? _authors.Max(a => a.Id) + 1 : 1; ;
             _authors.Add(author);
        }

        public void Delete(int id)
        {
            int affectedRows = _authors.RemoveAll(author => author.Id == id);

        }

        public IEnumerable<Author> GetAll()
        => _authors;

        public Author GetById(int id)
        => _authors.FirstOrDefault(author => author.Id == id);

        public void Update(Author author)
        {
           int authorId = _authors.FindIndex(authorItem => authorItem.Id == author.Id);
            if (authorId != -1)
            {
            _authors[authorId] = author;
            }
        }
        public bool ExistsAuthorByName(string name)
        {
            var authors = _authors.Where(author => author.Name == name);
            return authors.Any();
        }
        
    }
}
