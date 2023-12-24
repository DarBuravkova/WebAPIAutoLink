using WebAPIAutoLink.Data;
using WebAPIAutoLink.Interfaces;
using WebAPIAutoLink.Models;

namespace WebAPIAutoLink.Repository
{
    public class CarRepository : ICarRepository
    {
        private DataContext _context;
        public CarRepository(DataContext context)
        {
            _context = context;
        }
        public bool CarExists(int id)
        {
            return _context.Cars.Any(c => c.Id == id);
        }

        public bool CreateCar(Car car)
        {
            _context.Add(car);
            return Save();
        }

        public bool DeleteCar(Car car)
        {
            _context.Remove(car);
            return Save();
        }

        public bool DeleteCars(List<Car> cars)
        {
            _context.RemoveRange(cars);
            return Save();
        }

        public ICollection<Car> GetAvailableCars()
        {
            return _context.Cars.Where(e => e.IsRented == false).ToList();
        }

        public Car GetCar(int id)
        {
            return _context.Cars.Where(e => e.Id == id).FirstOrDefault();
        }

        public ICollection<Car> GetCars()
        {
            return _context.Cars.ToList();
        }

        public ICollection<Car> GetCarsByLocation(int locationId)
        {
            return _context.Cars.Where(e => e.Locations.Id == locationId).ToList();
        }

        public ICollection<Car> GetCarsByOwner(int ownerId)
        {
            return _context.Cars.Where(e => e.FleetOwners.Id == ownerId).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCar(Car car)
        {
            _context.Update(car);
            return Save();
        }
    }
}
