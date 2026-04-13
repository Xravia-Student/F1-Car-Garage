using System.Collections.Generic;

namespace F1_Car_Garage.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}