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
using StoreModels;

namespace StoreMVC.Controllers
{
    public class ManagerController : Controller
    {
        private IManagerBL _managerBL;
        private IMapper _mapper;

        public ManagerController(IManagerBL managerBL, IMapper mapper)
        {
            _managerBL = managerBL;
            _mapper = mapper;
        }
        // GET: ManagerController
        public ActionResult Index()
        {
            return View(_managerBL.GetManagers().Select(x => _mapper.cast2ManagerIndexVM(x)).ToList());
        }

        // GET: ManagerController/Details/5
        public ActionResult Details(string email)
        {
            if (email == null)
            {
                return View(_mapper.cast2ManagerCRVM(_managerBL.GetManagerByEmail(HttpContext.Session.GetString("UserEmail"))));
            }
            else
            {
                return View(_mapper.cast2ManagerCRVM(_managerBL.GetManagerByEmail(email)));
            }
        }

        // GET: ManagerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManagerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ManagerCRVM newManager)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _managerBL.AddManager(_mapper.cast2Manager(newManager));
                    //Helper.WriteInformation($"manager created-- Email: {newmanager.managerEmail}");
                    Log.Information($"Manager created-- Email: {newManager.ManagerEmail}");
                    return Redirect("/Manager/Login");
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

        // GET: ManagerController/Edit/5
        public ActionResult Edit(string email)
        {
            return View(_mapper.cast2ManagerEditVM(_managerBL.GetManagerByEmail(email)));
        }

        // POST: ManagerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ManagerEditVM manager2BUpdated)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (_mapper.verifyPW(_managerBL.GetManagerByEmail(manager2BUpdated.ManagerEmail).ManagerPasswordHash, manager2BUpdated.ManagerPasswordHash))
                    {
                        _managerBL.UpdateManager(_mapper.cast2Manager(manager2BUpdated));
                        Log.Information($"Manager updated-- Email: {manager2BUpdated.ManagerEmail}");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        Log.Information($"Manager not updated; incorrect password-- Email: {manager2BUpdated.ManagerEmail}");
                        return RedirectToAction(nameof(Index));
                    }
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

        // GET: ManagerController/Delete/5
        public ActionResult Delete(string email)
        {
            try
            {
                _managerBL.DeleteManager(_managerBL.GetManagerByEmail(email));
                Log.Information($"Manager deleted-- Email: {email}");
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

        // POST: ManagerController/Delete/5
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

        public ActionResult Login()
        {
            return View("Login");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM managerLogin)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Manager user = _managerBL.GetManagerByEmail(managerLogin.email);
                    if (user == null)
                    {
                        return RedirectToAction("Login");
                    }
                    else if (_mapper.verifyPW(_managerBL.GetManagerByEmail(managerLogin.email).ManagerPasswordHash, managerLogin.Password))
                    {
                        //return View("Details", _mapper.cast2ManagerCRVM(_managerBL.GetManagerByEmail(managerLogin.email)));
                        HttpContext.Session.SetString("UserName", user.ManagerName);
                        HttpContext.Session.SetString("UserEmail", user.ManagerEmail);
                        HttpContext.Session.SetInt32("UserId", user.Id);
                        HttpContext.Session.SetString("boolManager", "True");
                        return Redirect("/");
                    }
                    else
                    {
                        return RedirectToAction("Login");
                    }
                }
                catch
                {

                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Remove("UserEmail");
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("boolManager");
            HttpContext.Session.Remove("LocId");
            return Redirect("/");
        }
    }
}
