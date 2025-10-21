
using Library_Manager_DAL.Models;

namespace Library_Manager_DAL.Interfaces
{
    public interface IBookRepository:IRepository<Book>
    {

        bool ExistsBookByTitle(string title);
    }
}
