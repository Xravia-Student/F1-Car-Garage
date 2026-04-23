using F1_Car_Garage.Data;
using F1_Car_Garage.Models;
using F1_Car_Garage.Repository.IRepository;

namespace F1_Car_Garage.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public IPartRepository Parts { get; private set; }
        public IManufacturerRepository Manufacturers { get; private set; }
        public IRepository<Car> Cars { get; private set; }
        public IRepository<Racer> Racers { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Parts = new PartRepository(_db);
            Manufacturers = new ManufacturerRepository(_db);
            Cars = new Repository<Car>(_db);
            Racers = new Repository<Racer>(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}