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
// This interface defines the basic CRUD operations for any entity type T. It includes methods to get a single entity by ID, get all entities, add a new entity, remove an existing entity, and remove a range of entities. The generic type constraint ensures that T is a reference type (class).