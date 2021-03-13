using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreBL;
using StoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using WebErrorLogging.Utilities;

namespace StoreMVC.Controllers
{
    public class LocationController : Controller
    {
        private ILocationBL _locationBL;
        private IMapper _mapper;

        public LocationController(ILocationBL locationBL, IMapper mapper)
        {
            _locationBL = locationBL;
            _mapper = mapper;
        }
        // GET: LocationController
        public ActionResult Index()
        {
            return View(_locationBL.GetLocations().Select(x => _mapper.cast2LocationIndexVM(x)).ToList());
        }

        // GET: LocationController/Details/5
        public ActionResult Details(string name)
        {
           
            return View(_mapper.cast2LocationEditVM(_locationBL.GetLocationByName(name)));
            
        }

        // GET: LocationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LocationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LocCRVM newLocation)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _locationBL.AddLocation(_mapper.cast2Location(newLocation));
                    Log.Information($"Location created-- Name: {newLocation.LocName}");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    Helper.WriteError(e, "Error");
                    Helper.WriteFatal(e, "Fatal");
                    Helper.WriteVerbose(e, "Verbose");
                    return View();
                }
                finally
                {

                }
            }
            return View();
        }

        // GET: LocationController/Edit/5
        public ActionResult Edit(string name)
        {
            return View(_mapper.cast2LocationEditVM(_locationBL.GetLocationByName(name)));
        }

        // POST: LocationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LocEditVM location2BUpdated)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _locationBL.UpdateLocation(_mapper.cast2Location(location2BUpdated));
                    Log.Information($"Location updated-- Name: {location2BUpdated.LocName}");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    Helper.WriteError(e, "Error");
                    Helper.WriteFatal(e, "Fatal");
                    Helper.WriteVerbose(e, "Verbose");
                    return View();
                }
            }
            return View();
        }

        // GET: LocationController/Delete/5
        public ActionResult Delete(string name)
        {
            try
            {
                _locationBL.DeleteLocation(_locationBL.GetLocationByName(name));
                Log.Information($"Location deleted-- Name: {name}");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Helper.WriteError(e, "Error");
                Helper.WriteFatal(e, "Fatal");
                Helper.WriteVerbose(e, "Verbose");
                return View();
            }
        }

        // POST: LocationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Helper.WriteError(e, "Error");
                Helper.WriteFatal(e, "Fatal");
                Helper.WriteVerbose(e, "Verbose");
                return View();
            }
        }
    }
}
