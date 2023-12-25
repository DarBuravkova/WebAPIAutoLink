using WebAPIAutoLink.Models;

namespace WebAPIAutoLink.Interfaces
{
    public interface ILocationRepository
    {
        ICollection<Location> GetLocations();
        Location GetLocation(int id);
        bool LocationExists(int id);
        bool CreateLocation(Location location);
        bool UpdateLocation(Location location);
        bool DeleteLocation(Location location);
        bool Save();
    }
}
