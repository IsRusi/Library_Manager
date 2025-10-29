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


(Задание 4 часть 2) Система управления библиотекой

Необходимо обновить задание написанное на предыдущей неделе добавив работу с базой данных при помощи EF


1. Подключить EF Core

    - Использовать подход Code First

    - Использовать SQLite или MS SQL LocalDB

    - Создать `LibraryContext` и применить миграции


2. Сущности:

    - Настроить связь **один-ко-многим** между `Author` и `Book`

Пример:

public class Author

{

    public int Id { get; set; }

    public string Name { get; set; }

    public DateTime DateOfBirth { get; set; }

    public ICollection<Book> Books { get; set; }

}



3. Перевести логику с List<T> → EF Core

    - Заменить все обращения к спискам — на запросы в БД с помощью EF + LINQ


4. Использовать LINQ-запросы

    - Пример: получить всех авторов с количеством книг

    - Пример: получить книги, опубликованные после 2015 года

    - Пример: найти автора по имени (с `Contains` или `StartsWith`)


5. Работа с миграциями

    - Использовать `Add-Migration`, `Update-Database`


6. (Nice to have): Seed-данные (заполнить базу начальными авторами/книгами)