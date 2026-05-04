using F1_Car_Garage.Data;
using F1_Car_Garage.Repository.IRepository;
using System.Collections.Generic;

namespace F1_Car_Garage.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _db;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
        }

        public T Get(int id)
        {
            return _db.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _db.Set<T>().ToList();
        }

        public void Add(T entity)
        {
            _db.Set<T>().Add(entity);
        }

        public void Remove(T entity)
        {
            _db.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _db.Set<T>().RemoveRange(entities);
        }
    }
}
// This class implements the generic IRepository interface for any entity type T. It uses Entity Framework's DbContext to perform CRUD operations on the database. The methods include getting a single entity by ID, getting all entities, adding a new entity, removing an existing entity, and removing a range of entities. The generic type constraint ensures that T is a reference type (class).