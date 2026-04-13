namespace F1_Car_Garage.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IPartRepository Parts { get; }
        IManufacturerRepository Manufacturers { get; }
        IRepository<F1_Car_Garage.Models.Car> Cars { get; }
        void Save();
    }
}