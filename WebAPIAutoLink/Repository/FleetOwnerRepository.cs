using WebAPIAutoLink.Data;
using WebAPIAutoLink.Interfaces;
using WebAPIAutoLink.Models;

namespace WebAPIAutoLink.Repository
{
    public class FleetOwnerRepository : IFleetOwnerRepository
    {
        private readonly DataContext _context;

        public FleetOwnerRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateFleetOwner(FleetOwner fleetOwner)
        {
            _context.Add(fleetOwner);
            return Save();
        }

        public bool DeleteFleetOwner(FleetOwner fleetOwner)
        {
            _context.Remove(fleetOwner);
            return Save();
        }

        public bool FleetOwnerExists(int fleetOwnerId)
        {
            return _context.FleetOwners.Any(o => o.Id == fleetOwnerId);
        }

        public ICollection<Car> GetCarsByOwner(int ownerId)
        {
            return _context.Cars.Where(p => p.FleetOwners.Id == ownerId).ToList();
        }

        public FleetOwner GetFleetOwner(int fleetOwnerId)
        {
            return _context.FleetOwners.Where(o => o.Id == fleetOwnerId).FirstOrDefault();
        }

        public FleetOwner GetFleetOwnerOfACar(int carId)
        {
            return _context.Cars.Where(o => o.Id == carId).Select(c => c.FleetOwners).FirstOrDefault();
        }

        public ICollection<FleetOwner> GetFleetOwners()
        {
            return _context.FleetOwners.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateFleetOwner(FleetOwner fleetOwner)
        {
            _context.Update(fleetOwner);
            return Save();
        }
    }
}
