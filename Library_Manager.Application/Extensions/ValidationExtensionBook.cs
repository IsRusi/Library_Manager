using Library_Manager.Application.DTO;
using Library_Manager.Application.Interfaces.IService;
using Library_Manager.Domain.Models;

namespace Library_Manager.Application.Extensions
{
    public static class ValidationExtensionBook
    {
        public static void ValidateBook(this BookDTO book)
        {
            if (book == null)
                throw new ArgumentNullException($"Книга с таким id {book?.Id} не существует");
        }

        public static void ValidateId(this int id)
        {
            if (id < 0)
                throw new ArgumentException("Id не может быть отрицательным");
        }

        public static async Task ValidateTitleAsync(this string title,IBookService bookService)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Название книги не может быть пустым");

            if (await bookService.ExistsBookByTitleAsync(title))
                throw new ArgumentException("Книга с таким названием уже существует");
        }

        public static void ValidatePublishedYear(this int year)
        {
            if (year < 0 || year > DateTime.UtcNow.Year)
                throw new ArgumentException("Год публикации указан некорректно");
        }

    }
}
