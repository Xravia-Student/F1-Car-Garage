using F1_Car_Garage.Models;
using System.Collections.Generic;

namespace F1_Car_Garage.Repository.IRepository
{
    public interface IManufacturerRepository : IRepository<Manufacturer>
    {
        IEnumerable<Manufacturer> GetAllWithParts();
    }
}