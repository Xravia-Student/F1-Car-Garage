using F1_Car_Garage.Models;
using System.Collections.Generic;

namespace F1_Car_Garage.Repository.IRepository
{
    public interface IPartRepository : IRepository<Part>
    {
        IEnumerable<Part> GetAllWithManufacturer();
        Part? GetWithManufacturer(int id);
    }
}
// This interface extends the generic IRepository for Part entities and adds methods to retrieve parts along with their associated manufacturer information.