namespace F1_Car_Garage.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IPartRepository Parts { get; }
        IManufacturerRepository Manufacturers { get; }
        IRepository<F1_Car_Garage.Models.Car> Cars { get; }
        IRepository<F1_Car_Garage.Models.Racer> Racers { get; }
        void Save();
    }
}
// The IUnitOfWork interface defines a contract for a unit of work pattern, which is used to group multiple repository operations into a single transaction. It provides properties for accessing specific repositories (Parts, Manufacturers, Cars, Racers) and a Save method to commit changes to the database. It also implements IDisposable to allow for proper resource management.