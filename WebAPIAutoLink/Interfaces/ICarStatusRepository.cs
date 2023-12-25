using WebAPIAutoLink.Models;

namespace WebAPIAutoLink.Interfaces
{
    public interface ICarStatusRepository
    {
        CarStatus GetCarStatus(int carId);
        bool CarStatusExists(int id);
        bool CreateCarStatus(CarStatus carStatus);
        bool UpdateCarStatus(CarStatus carStatus);
        bool Save();
    }
}
