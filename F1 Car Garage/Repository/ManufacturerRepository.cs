using F1_Car_Garage.Data;
using F1_Car_Garage.Models;
using F1_Car_Garage.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace F1_Car_Garage.Repository
{
    public class ManufacturerRepository : Repository<Manufacturer>, IManufacturerRepository
    {
        public ManufacturerRepository(ApplicationDbContext db) : base(db)
        {
        }

        public IEnumerable<Manufacturer> GetAllWithParts()
        {
            return _db.Manufacturers.Include(m => m.Parts).ToList();
        }
    }
}
// This class implements the IManufacturerRepository interface and extends the generic Repository class for Manufacturer entities. It provides an implementation for the GetAllWithParts method, which retrieves all manufacturers along with their associated parts using Entity Framework's Include method to perform eager loading.