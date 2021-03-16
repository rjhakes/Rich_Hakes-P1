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
        private ICustomerCartBL _cartBL;
        private ICustomerOrderLineItemBL _orderLineItemBL;
        private ICustomerOrderLineItemBL _customerOrderLineItemBL;
        private ILocationBL _locationBL;
        private IProductBL _productBL;
        private ICustomerOrderHistoryBL _customerOrderHistoryBL;
        private IInventoryLineItemBL _inventoryLineItemBL;
        private IMapper _mapper;
        
        public CustomerController(ICustomerBL customerBL, ICustomerCartBL cartBL, 
            ICustomerOrderLineItemBL orderLineItemBL, ICustomerOrderLineItemBL customerOrderLineItemBL, ILocationBL locationBL,
            IProductBL productBL, ICustomerOrderHistoryBL customerOrderHistoryBL, IInventoryLineItemBL inventoryLineItemBL, IMapper mapper)
        {
            _customerBL = customerBL;
            _cartBL = cartBL;
            _orderLineItemBL = orderLineItemBL;
            _customerOrderLineItemBL = customerOrderLineItemBL;
            _locationBL = locationBL;
            _productBL = productBL;
            _customerOrderHistoryBL = customerOrderHistoryBL;
            _inventoryLineItemBL = inventoryLineItemBL;
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
            return View("Create");
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
                    Log.Information($"Customer created-- Email: {newCustomer.CustomerEmail}");

                    //move this loop to BL
                    foreach (var loc in _locationBL.GetLocations())
                    {
                        CustomerCart cart = new CustomerCart();
                        cart.CustId = _customerBL.GetCustomerByEmail(newCustomer.CustomerEmail).Id;
                        cart.LocId = loc.Id;
                        cart.CurrentItemsId = _orderLineItemBL.Ident_Curr() + 1;
                        _cartBL.AddCustomerCart(cart);
                        CustomerOrderLineItem orderLineItem = new CustomerOrderLineItem();
                        orderLineItem.OrderId = cart.CurrentItemsId;
                        orderLineItem.ProdId = null;
                        orderLineItem.Quantity = 0;
                        orderLineItem.ProdPrice = 0;
                        _customerOrderLineItemBL.AddCustomerOrderLineItem(orderLineItem);
                    }
                    //Helper.WriteInformation($"Customer created-- Email: {newCustomer.CustomerEmail}");
                    
                    return Redirect("/Customer/Login");
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
                        return RedirectToAction("Login");
                    }
                    else if (_mapper.verifyPW(_customerBL.GetCustomerByEmail(customerLogin.email).CustomerPasswordHash, customerLogin.Password))
                    {
                        //return View("Details", _mapper.cast2CustomerCRVM(_customerBL.GetCustomerByEmail(customerLogin.email)));
                        HttpContext.Session.SetString("UserName", user.CustomerName);
                        HttpContext.Session.SetString("UserEmail", user.CustomerEmail);
                        HttpContext.Session.SetInt32("UserId", user.Id);
                        HttpContext.Session.SetString("boolManager", "False");
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

        public ActionResult Cart()
        {
            if (HttpContext.Session.GetInt32("LocId") != null)
            {
                try
                {
                    int locId = (int)HttpContext.Session.GetInt32("LocId");
                    return View(_customerOrderLineItemBL.GetCustomerOrderLineItemById
                        (_cartBL.GetCustomerCartByIds(_customerBL.GetCustomerByEmail(HttpContext.Session.GetString("UserEmail")).Id, locId).CurrentItemsId)
                        .Select(x => _mapper.cast2CartIndexVM(x,
                            _productBL.GetProductById((int)x.ProdId)
                        )).ToList());
                }
                catch
                {
                    return Redirect($"/InventoryLineItem?locId={HttpContext.Session.GetInt32("LocId")}");
                }
                
            }
            return Redirect("/");
            
        }

        public ActionResult Purchase(int itemList, double total)
        {
            CustomerOrderHistory coh = new CustomerOrderHistory();
            coh.LocId = (int)HttpContext.Session.GetInt32("LocId");
            coh.CustId = _customerBL.GetCustomerByEmail(HttpContext.Session.GetString("UserEmail")).Id;
            coh.OrderDate = DateTime.Now;
            coh.OrderId = itemList;
            coh.Total = total;
            _customerOrderHistoryBL.AddCustomerOrderHistory(coh);
            CustomerCart cart = new CustomerCart();
            /*cart.CustId = coh.CustId;
            cart.LocId = coh.LocId;*/
            cart = _cartBL.GetCustomerCartByIds(coh.CustId, coh.LocId);
            cart.CurrentItemsId = _orderLineItemBL.Ident_Curr() + 1;
            _cartBL.UpdateCustomerCart(cart);
            CustomerOrderLineItem orderLineItem = new CustomerOrderLineItem();
            orderLineItem.OrderId = cart.CurrentItemsId;
            orderLineItem.ProdId = null;
            orderLineItem.Quantity = 0;
            orderLineItem.ProdPrice = 0;
            _customerOrderLineItemBL.AddCustomerOrderLineItem(orderLineItem);
            return Redirect($"/InventoryLineItem?locId={HttpContext.Session.GetInt32("LocId")}");
        }

        public ActionResult OrderHistory(string email)
        {

            return View
                (
                _customerOrderHistoryBL.GetCustomerOrderHistoryById
                    (
                        _customerBL.GetCustomerByEmail(HttpContext.Session.GetString("UserEmail")).Id
                    ).Select
                        (
                            x => _mapper.cast2OrderHistoryIndexVM
                            (
                                x, 
                                _locationBL.GetLocationById(x.LocId), 
                                _customerOrderLineItemBL.GetCustomerOrderLineItemById(x.OrderId).Select
                                (y => _mapper.cast2CartIndexVM(y,
                                    _productBL.GetProductById((int)y.ProdId)
                                )).ToList()
                            )
                    ).ToList()
                
                );
        }

        public ActionResult OrderDetails(int id)
        {
            return View
                (
                    _customerOrderLineItemBL.GetCustomerOrderLineItemById
                        (id)
                        .Select(x => _mapper.cast2CartIndexVM(x,
                            _productBL.GetProductById((int)x.ProdId)
                        )).ToList()
                );
        }

        public ActionResult DeleteItem(int id, string prodName)
        {
            try
            {
                InventoryLineItem iLI = _inventoryLineItemBL.GetInventoryLineItemById
                    ((int)HttpContext.Session.GetInt32("LocId"), (int)_customerOrderLineItemBL.GetCustomerOrderLineItemById(id, _productBL.GetProductByName(prodName).Id).ProdId);
                iLI.Quantity += _customerOrderLineItemBL.GetCustomerOrderLineItemById(id, _productBL.GetProductByName(prodName).Id).Quantity;
                _inventoryLineItemBL.UpdateInventoryLineItem(iLI);
                _orderLineItemBL.DeleteCustomerOrderLineItem(_customerOrderLineItemBL.GetCustomerOrderLineItemById(id, _productBL.GetProductByName(prodName).Id));

                //Log.Information($"Customer deleted-- Email: {email}");
                return RedirectToAction("Cart");
            }
            catch (Exception e)
            {
                /*Helper.WriteError(e, "Error");
                Helper.WriteFatal(e, "Fatal");
                Helper.WriteVerbose(e, "Verbose");*/

                CustomerCart cart = new CustomerCart();
                cart.CustId = _customerBL.GetCustomerByEmail(HttpContext.Session.GetString("UserEmail")).Id;
                cart.LocId = _locationBL.GetLocationById((int)HttpContext.Session.GetInt32("LocId")).Id;
                cart.CurrentItemsId = _orderLineItemBL.Ident_Curr() + 1;
                _cartBL.UpdateCustomerCart(cart);
                CustomerOrderLineItem orderLineItem = new CustomerOrderLineItem();
                orderLineItem.OrderId = cart.CurrentItemsId;
                orderLineItem.ProdId = null;
                orderLineItem.Quantity = 0;
                orderLineItem.ProdPrice = 0;
                _customerOrderLineItemBL.AddCustomerOrderLineItem(orderLineItem);
                return Redirect($"/InventoryLineItem?locId={HttpContext.Session.GetInt32("LocId")}");
            }
            //return View();
        }
    }
}
