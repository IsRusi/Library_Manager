using Library_Manager.Application.DTO;
using Library_Manager.Application.Interfaces.IService;
using Library_Manager.Application.Service;
using System.Xml.Linq;

namespace Library_Manager.Application.Extensions
{
    public static class ValidationExtensionAuthor
    {
        public static void ValidateAuthor(this AuthorDTO author)
        {
            if (author == null)
            {
                throw new ArgumentNullException($"Автора с таким id {author?.Id} не существует");
            }
        }
        public static void ValidateId(this int id)
        {
            if (id < 0)
                throw new($"id не может быть отрицательным");
        }
        public static void ValidateDate(this DateOnly dateOfBirth)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            if (dateOfBirth >= today)
            {
                throw new("Дата рождения не может быть сегодняшней или будущей.");
            }
        }

        public static async Task ValidateNameAsync(this string name, IAuthorService authorService)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                throw new("Название не может быть пустым");
            }
            if (await authorService.ExistsAuthorByNameAsync(name))
            {
                throw new("Автор с таким именем уже существует");
            }
        }
    }
}
