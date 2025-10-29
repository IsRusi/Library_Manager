namespace Library_Manager.Application.Interfaces.IRepository
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();

        Task UpdateAsync(T entity);

        Task DeleteAsync(int id);

        Task AddAsync(T entity);
    }
}