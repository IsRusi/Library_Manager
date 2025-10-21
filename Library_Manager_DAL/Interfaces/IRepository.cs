namespace Library_Manager_DAL.Interfaces
{
    public interface IRepository<T>
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        void Update(T entity);
        void Delete(int id);
        void Add(T entity);
    }
}
