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
    public class InventoryLineItemController : Controller
    {
        private IInventoryLineItemBL _inventoryLineItemBL;
        private IProductBL _productBL;
        private ICustomerBL _customerBL;
        private ICustomerOrderLineItemBL _coliBL;
        private ICustomerCartBL _cartBL;
        private IMapper _mapper;

        public InventoryLineItemController(IInventoryLineItemBL inventoryLineItemBL, IProductBL productBL,
            ICustomerBL customerBL, ICustomerOrderLineItemBL coliBL, ICustomerCartBL cartBL, IMapper mapper)
        {
            _inventoryLineItemBL = inventoryLineItemBL;
            _productBL = productBL;
            
            _customerBL = customerBL;
            _coliBL = coliBL;
            _cartBL = cartBL;
            _mapper = mapper;
        }
        // GET: InventoryController
        public ActionResult Index(int locId)
        {
            return View(_inventoryLineItemBL.GetInventoryLineItems()
                .Where(x => x.InventoryId == locId)
                .Select(x => _mapper.cast2InventoryLineItemIndexVM(x, _productBL.GetProductById(x.ProductId)))
                .OrderBy(x => x.ProductId)
                .ToList());
        }

        // GET: InventoryController/Details/5
        public ActionResult Details(int id, int prodId)
        {
            return View(_mapper
                .cast2OrderItemVM(_inventoryLineItemBL.GetInventoryLineItemById(id), _productBL.GetProductById(prodId)));
        }
        /*[HttpPost]
        [ValidateAntiForgeryToken]*/
        public ActionResult AddToCart(int id, int amount) //(OrderItemVM item, int amount)
        {
            /*_executeOrder.AddItemToCart(_customerBL.GetCustomerByEmail(HttpContext.Session.GetString("UserEmail")),
                _inventoryLineItemBL.GetInventoryLineItemById(id), amount);*/

            //move to BL
            try
            {
                if (_inventoryLineItemBL.GetInventoryLineItemById(
                _inventoryLineItemBL.GetInventoryLineItemById(id).InventoryId,
                _inventoryLineItemBL.GetInventoryLineItemById(id).ProductId).Quantity > 0)
                {
                    bool updateCOLI = false;
                    CustomerOrderLineItem coli = new CustomerOrderLineItem();
                    Customer customer = new Customer();
                    CustomerCart cart = new CustomerCart();
                    customer = _customerBL.GetCustomerByEmail(HttpContext.Session.GetString("UserEmail"));
                    cart = _cartBL.GetCustomerCartByIds(customer.Id, _inventoryLineItemBL.GetInventoryLineItemById(id).InventoryId);

                    coli.OrderId = cart.CurrentItemsId;
                    coli.ProdId = _inventoryLineItemBL.GetInventoryLineItemById(id).ProductId;
                    coli.Quantity = amount;
                    coli.ProdPrice = _productBL.GetProductById(_inventoryLineItemBL.GetInventoryLineItemById(id).ProductId).ProdPrice;

                    foreach (var item in _coliBL.GetCustomerOrderLineItemById(cart.CurrentItemsId))
                    {
                        if (item.ProdId == null)
                        {
                            coli = item;
                            coli.ProdId = _inventoryLineItemBL.GetInventoryLineItemById(id).ProductId;
                            coli.Quantity = amount;
                            coli.ProdPrice = _productBL.GetProductById(_inventoryLineItemBL.GetInventoryLineItemById(id).ProductId).ProdPrice;
                            updateCOLI = true;
                            break;
                        }
                        else if (item.ProdId == _inventoryLineItemBL.GetInventoryLineItemById(id).ProductId)
                        {
                            coli = item;
                            coli.Quantity += amount;
                            updateCOLI = true;
                            break;
                        }
                    }
                    if (updateCOLI)
                    {
                        _coliBL.UpdateCustomerOrderLineItem(coli);
                    }
                    else
                    {
                        _coliBL.AddCustomerOrderLineItem(coli);
                    }
                    InventoryLineItem updateILI = _inventoryLineItemBL.GetInventoryLineItemById(id);
                    updateILI.Quantity -= 1;
                    _inventoryLineItemBL.UpdateInventoryLineItem(updateILI);
                    return RedirectToAction("Index", new { locId = _inventoryLineItemBL.GetInventoryLineItemById(id).InventoryId });
                }
                else
                {
                    return RedirectToAction("Index", new { locId = _inventoryLineItemBL.GetInventoryLineItemById(id).InventoryId });
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", new { locId = _inventoryLineItemBL.GetInventoryLineItemById(id).InventoryId });
            }
            


            /*coli = _coliBL.GetCustomerOrderLineItemById(cart.CurrentItemsId);
            if (coli.ProdId == null)
            {
                coli.ProdId = 
            }*/
            
        }

        // GET: InventoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InventoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InvLineItemCRVM newInventoryLineItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _inventoryLineItemBL.AddInventoryLineItem(_mapper.cast2InventoryLineItem(newInventoryLineItem));
                    //Helper.WriteInformation($"InventoryLineItem created-- Email: {newInventoryLineItem.InventoryLineItemEmail}");
                    //Log.Information($"InventoryLineItem created-- Email: {newInventoryLineItem.InventoryLineItemEmail}");
                    return RedirectToAction("Index", new { locId = newInventoryLineItem.InventoryId });
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

        // GET: InventoryController/Edit/5
        public ActionResult Edit(int id)
        {
            //return View();
            return View(_mapper
                .cast2InventoryLineItemEditVM(_inventoryLineItemBL.GetInventoryLineItemById(id), _productBL.GetProductById(_inventoryLineItemBL.GetInventoryLineItemById(id).ProductId)));
        }

        // POST: InventoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InvLineItemEditVM inventoryLineItem2BUpdated)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _inventoryLineItemBL.UpdateInventoryLineItem(_mapper.cast2InventoryLineItem(inventoryLineItem2BUpdated));
                    Log.Information($"InventoryLineItem updated-- Id: {inventoryLineItem2BUpdated.Id}");
                    return RedirectToAction("Index", new { locId = inventoryLineItem2BUpdated.InventoryId });
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

        // GET: InventoryController/Delete/5
        public ActionResult Delete(int invId, int prodId)
        {
            try
            {
                _inventoryLineItemBL.DeleteInventoryLineItem(_inventoryLineItemBL.GetInventoryLineItemById(invId, prodId));
                Log.Information($"Inventory Item deleted--");
                return RedirectToAction("Index", new { locId = invId });
            }
            catch (Exception e)
            {
                Helper.WriteError(e, "Error");
                Helper.WriteFatal(e, "Fatal");
                Helper.WriteVerbose(e, "Verbose");
                return View();
            }
        }

        // POST: InventoryController/Delete/5
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
