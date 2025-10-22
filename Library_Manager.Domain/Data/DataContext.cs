using Library_Manager.Domain.Models;

namespace Library_Manager.Domain.Data
{
    public class DataContext
    {
        public List<Author> Author { get; }
        public List<Book> Book { get; }

    }
}

