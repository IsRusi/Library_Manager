# Library Manager
## Техническое Задание

### (Задание 4 часть 1) Система управления библиотекой

1. Создать ASP.NET Core Web API (без Razor/Blazor/MVC UI):
Проект типа `ASP.NET Core Web API` 
2. Создать модели в коде
    `Author`: `Id`, `Name`, `DateOfBirth`
    `Book`: `Id`, `Title`, `PublishedYear`, `AuthorId`
3. Создать временное хранилище
    Использовать **временные списки в памяти (List<T>)** вместо базы данных.
4. Создать контроллеры
    `AuthorsController` и `BooksController`
    CRUD-операции:
     - Получение всех
     - Получение по ID
     - Создание
     - Обновление
     - Удаление
5. Протестировать через Swagger или Postman
6. (Nice to have): Реализовать базовую валидацию (например, не создавать книги без названия).
