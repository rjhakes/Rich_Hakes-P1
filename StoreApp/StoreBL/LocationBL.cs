using System;
using System.Collections.Generic;
using StoreDL;
using StoreModels;

namespace StoreBL
{
    public class LocationBL : ILocationBL
    {
        private ILocationRepository _repo;

        public LocationBL(ILocationRepository repo) {
            _repo = repo;
        }

        public Location AddLocation(Location newLocation)
        {
            //TODO: Add BL
            return _repo.AddLocation(newLocation);
        }
        public Location DeleteLocation(Location location2BDeleted)
        {
            return _repo.DeleteLocation(location2BDeleted);
        }
        public Location GetLocationByName(string name) {
            //todo validate
            return _repo.GetLocationByName(name);
        }
        public Location GetLocationById(int id) {
            //todo validate
            return _repo.GetLocationById(id);
        }
        public List<Location> GetLocations()
        {
            //TODO Add BL
            return _repo.GetLocations();
        }
        public Location UpdateLocation(Location location2BUpdated)
        {
            return _repo.UpdateLocation(location2BUpdated);
        }
    }
}