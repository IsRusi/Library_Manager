using Library_Manager_DAL.Models;

namespace Library_Manager_DAL.Interfaces
{
    public interface IAuthorRepository:IRepository<Author>
    {
        bool ExistsAuthorByName(string name);
    }
}
