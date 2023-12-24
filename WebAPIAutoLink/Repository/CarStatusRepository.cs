using WebAPIAutoLink.Data;
using WebAPIAutoLink.Interfaces;
using WebAPIAutoLink.Models;

namespace WebAPIAutoLink.Repository
{
    public class CarStatusRepository : ICarStatusRepository
    {
        private DataContext _context;
        public CarStatusRepository(DataContext context)
        {
            _context = context;
        }
        public bool CarStatusExists(int id)
        {
            return _context.CarsStatuss.Any(c => c.Id == id);
        }

        public bool CreateCarStatus(CarStatus carStatus)
        {
            _context.Add(carStatus);
            return Save();
        }

        public bool DeleteCarStatus(CarStatus carStatus)
        {
            _context.Remove(carStatus);
            return Save();
        }

        public CarStatus GetCarStatus(int carId)
        {
            return _context.CarsStatuss.Where(o => o.CarId == carId).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCarStatus(CarStatus carStatus)
        {
            _context.Update(carStatus);
            return Save();
        }
    }
}
