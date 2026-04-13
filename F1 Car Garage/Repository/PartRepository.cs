using F1_Car_Garage.Data;
using F1_Car_Garage.Models;
using F1_Car_Garage.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace F1_Car_Garage.Repository
{
    public class PartRepository : Repository<Part>, IPartRepository
    {
        public PartRepository(ApplicationDbContext db) : base(db)
        {
        }

        public IEnumerable<Part> GetAllWithManufacturer()
        {
            return _db.Parts.Include(p => p.Manufacturer).ToList();
        }

        public Part? GetWithManufacturer(int id)
        {
            return _db.Parts.Include(p => p.Manufacturer)
                             .FirstOrDefault(p => p.PartId == id);
        }
    }
}