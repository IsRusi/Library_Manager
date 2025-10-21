
using Library_Manager_DAL.Interfaces;
using Library_Manager_DAL.Models;
using Microsoft.Extensions.Logging;

namespace Library_Manager_BLL.Services
{
    public class AuthorService:IAuthorService
    {
        private readonly ILogger<AuthorService> _logger;
        private readonly IAuthorRepository _authorRepository;
        public AuthorService(IAuthorRepository authorRepository, ILogger<AuthorService> logger)
        {
            _authorRepository = authorRepository;
            _logger = logger;
        }

        public Author AddAuthor(string authorName, DateOnly authorDateOfBirth)
        {
            try
            {

                ValidateName(authorName);
                ValidateDate(authorDateOfBirth);
                var newAuthorToAdd = new Author()
                {
                    Name = authorName,
                    DateOfBirth = authorDateOfBirth,
                };
                _authorRepository.Add(newAuthorToAdd);
                return newAuthorToAdd;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public void DeleteAuthorById(int id)
        {
            try
            {
                ValidateId(id);
                var authorToDelete = _authorRepository.GetById(id);
                ValidateAuthor(authorToDelete);
                _authorRepository.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<Author> GetAllAuthors()
        => _authorRepository.GetAll();


        public Author GetById(int id)
        {
            try {
                ValidateId(id);
                var author = _authorRepository.GetById(id);
                ValidateAuthor(author);
                return author;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public void Update(int id,Author updateAuthor)
        {
            try
            {
                var authorToUpdate = _authorRepository.GetById(id);

                ValidateAuthor(authorToUpdate);

                ValidateName(updateAuthor.Name);
                ValidateDate(updateAuthor.DateOfBirth);

                authorToUpdate.Name = updateAuthor.Name;
                authorToUpdate.DateOfBirth = updateAuthor.DateOfBirth;

                _authorRepository.Update(authorToUpdate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        private void ValidateAuthor(Author author)
        {
            if(author == null)
            {
                throw new ArgumentNullException($"Автора с таким id {author?.Id} не существует");
            }
        }
        private void ValidateId(int id)
        {
            if(id < 0)
            throw new($"id не может быть отрицательным");
        }
        private void ValidateDate(DateOnly dateOfBirth)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            if (dateOfBirth >= today)
            {
                throw new ("Дата рождения не может быть сегодняшней или будущей.");
            }
        }
        private void ValidateName(string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                throw new("Название не может быть пустым");
            }
            if(_authorRepository.ExistsAuthorByName(name))
            {
                throw new("Автор с таким именем уже существует");
            }
        }
    }
}
