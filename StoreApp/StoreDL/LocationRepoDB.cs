using StoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDL
{
    public class LocationRepoDB : ILocationRepository
    {
        private readonly StoreDBContext _context;
        public LocationRepoDB(StoreDBContext context)
        {
            _context = context;
        }
        public Location AddLocation(Location newLocation)
        {
            _context.Locations.Add(newLocation);
            _context.SaveChanges();
            return newLocation;
        }

        public Location DeleteLocation(Location location2BDeleted)
        {
            _context.Locations.Remove(location2BDeleted);
            _context.SaveChanges();
            return location2BDeleted;
        }

        public Location GetLocationByName(string name)
        {
            return _context.Locations
                .FirstOrDefault(x => x.LocName == name);
        }

        public Location GetLocationById(int id)
        {
            return _context.Locations
                .FirstOrDefault(x => x.Id == id);
        }

        public List<Location> GetLocations()
        {
            return _context.Locations
                .Select(x => x)
                .ToList();
        }

        public Location UpdateLocation(Location location2BUpdated)
        {
            Location oldLocation = _context.Locations.Find(location2BUpdated.Id);


            _context.Entry(oldLocation).CurrentValues.SetValues(location2BUpdated);

            _context.SaveChanges();

            //This method clears the change tracker to drop all tracked entities
            _context.ChangeTracker.Clear();
            return location2BUpdated;

        }
    }
}
