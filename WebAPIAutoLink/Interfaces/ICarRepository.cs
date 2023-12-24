using WebAPIAutoLink.Models;

namespace WebAPIAutoLink.Interfaces
{
    public interface ICarRepository
    {
        ICollection<Car> GetCars();
        Car GetCar(int id);
        ICollection<Car> GetCarsByOwner(int ownerId);
        ICollection<Car> GetAvailableCars();
        ICollection<Car> GetCarsByLocation(int locationId);
        bool CarExists(int id);
        bool CreateCar(Car car);
        bool UpdateCar(Car car);
        bool DeleteCar(Car car);
        bool DeleteCars(List<Car> cars);
        bool Save();
    }
}
