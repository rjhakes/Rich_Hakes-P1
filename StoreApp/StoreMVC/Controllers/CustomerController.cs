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
    public class CustomerController : Controller
    {

        private ICustomerBL _customerBL;
        private IMapper _mapper;
        
        public CustomerController(ICustomerBL customerBL, IMapper mapper)
        {
            _customerBL = customerBL;
            _mapper = mapper;
        }
        
        // GET: CustomerController
        public ActionResult Index()
        {
            //add view Index after migrating DB 
            return View(_customerBL.GetCustomers().Select(x => _mapper.cast2CustomerIndexVM(x)).ToList());
        }

        // GET: CustomerController/Details?email={customerEmail}
        public ActionResult Details(string email)
        {
            //add view Details
            if (email == null)
            {
                return View(_mapper.cast2CustomerCRVM(_customerBL.GetCustomerByEmail(HttpContext.Session.GetString("UserEmail"))));
            }
            else
            {
                return View(_mapper.cast2CustomerCRVM(_customerBL.GetCustomerByEmail(email)));
            }
            
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            //add view CreateCustomer
            return View("CreateCustomer");
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerCRVM newCustomer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _customerBL.AddCustomer(_mapper.cast2Customer(newCustomer));
                    //Helper.WriteInformation($"Customer created-- Email: {newCustomer.CustomerEmail}");
                    Log.Information($"Customer created-- Email: {newCustomer.CustomerEmail}");
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

        // GET: CustomerController/Edit/5
        public ActionResult Edit(string email)
        {
            return View(_mapper.cast2CustomerEditVM(_customerBL.GetCustomerByEmail(email)));
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerEditVM customer2BUpdated)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (_mapper.verifyPW(_customerBL.GetCustomerByEmail(customer2BUpdated.CustomerEmail).CustomerPasswordHash, customer2BUpdated.CustomerPasswordHash))
                    {
                        _customerBL.UpdateCustomer(_mapper.cast2Customer(customer2BUpdated));
                        Log.Information($"Customer updated-- Email: {customer2BUpdated.CustomerEmail}");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        Log.Information($"Customer not updated; incorrect password-- Email: {customer2BUpdated.CustomerEmail}");
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

        // GET: CustomerController/Delete
        public ActionResult Delete(string email)
        {
            try
            {
                _customerBL.DeleteCustomer(_customerBL.GetCustomerByEmail(email));
                Log.Information($"Customer deleted-- Email: {email}");
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

        // POST: CustomerController/Delete/5
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
        public ActionResult Login(LoginVM customerLogin)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Customer user = _customerBL.GetCustomerByEmail(customerLogin.email);
                    if (user == null)
                    {
                        return NotFound();
                    }
                    else if (_mapper.verifyPW(_customerBL.GetCustomerByEmail(customerLogin.email).CustomerPasswordHash, customerLogin.Password))
                    {
                        //return View("Details", _mapper.cast2CustomerCRVM(_customerBL.GetCustomerByEmail(customerLogin.email)));
                        HttpContext.Session.SetString("UserName", user.CustomerName);
                        HttpContext.Session.SetString("UserEmail", user.CustomerEmail);
                        HttpContext.Session.SetInt32("UserId", user.Id);
                        return Redirect("/");
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
            return RedirectToAction("Login");
        }
    }
}
