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