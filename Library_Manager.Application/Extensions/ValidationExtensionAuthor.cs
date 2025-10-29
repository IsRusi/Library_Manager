using Library_Manager.Application.DTO;
using Library_Manager.Application.Exceptions;
using Library_Manager.Application.Interfaces.IService;
using Library_Manager.Domain.Models;

namespace Library_Manager.Application.Extensions
{
    public static class ValidationExtensionAuthor
    {
        private const int AGE_MAX = 130;
        private const int AGE_MIN = 0;

        public static void ValidateAuthorsBooks(this Author author)
        {
            if (author.Books.Any())
                throw new InvalidAuthorOperationException("Нельзя удалить автора с книгами.");
        }
        public static void ValidateAuthor(this AuthorDTO author)
        {
            if (author == null)
            {
                throw new AuthorNotFoundException($"Автора с таким id {author?.Id} не существует");
            }
        }

        public static void ValidateId(this int id)
        {
            if (id < 0)
                throw new InvalidIdException($"id не может быть отрицательным");
        }

        public static void ValidateDate(this DateTime dateOfBirth)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            if (DateOnly.FromDateTime(dateOfBirth) >= today)
            {
                throw new InvalidDateOfBirthException("Дата рождения не может быть сегодняшней или будущей.");
            }
        }
        public static void ValidateAge(this int age)
        {

            if (age<AGE_MIN || age>AGE_MAX)
            {
                throw new InvalidAgeException("Возраст некорректен");
            }
        }
        public static async Task ValidateNameAsync(this string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                throw new InvalidNameException("Название не может быть пустым");
            }
        }
        public static async Task ValidateNameAsync(this string name, IAuthorService authorService)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                throw new InvalidNameException("Название не может быть пустым");
            }
            if (await authorService.ExistsAuthorByNameAsync(name))
            {
                throw new AuthorAlreadyExistsException("Автор с таким именем уже существует");
            }
        }
    }
}