using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreBL;
using StoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return View(_mapper.cast2CustomerCRVM(_customerBL.GetCustomerByEmail(email)));
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
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            return View();
            
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Delete
        public ActionResult Delete(string email)
        {
            _customerBL.DeleteCustomer(_customerBL.GetCustomerByEmail(email));
            return RedirectToAction(nameof(Index));
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
            catch
            {
                return View();
            }
        }
    }
}
