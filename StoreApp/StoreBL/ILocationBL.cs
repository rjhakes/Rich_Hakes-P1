using StoreModels;
using System.Collections.Generic;

namespace StoreBL
{
    public interface ILocationBL
    {
        List<Location> GetLocations();
        Location AddLocation(Location newLocation);
        Location GetLocationByName(string name);
        Location GetLocationById(int id);
        Location DeleteLocation(Location location2BDeleted);
        Location UpdateLocation(Location location2BUpdated);
    }
}