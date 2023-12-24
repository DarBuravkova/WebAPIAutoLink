using WebAPIAutoLink.Models;

namespace WebAPIAutoLink.Interfaces
{
    public interface ILocationRepository
    {
        ICollection<Location> GetLocations();
        Location GetLocation(int id);
        ICollection<Car> GetCarsFromALocation(int locationId);
        bool LocationExists(int id);
        bool CreateLocation(Location location);
        bool UpdateLocation(Location location);
        bool DeleteLocation(Location location);
        bool Save();
    }
}
