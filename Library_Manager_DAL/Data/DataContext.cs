using Library_Manager_DAL.Models;

namespace Library_Manager_DAL.Data
{
    internal class DataContext
    {
        public List<Author> Author { get; }
        public List<Book> Book{ get; }

    }
}
