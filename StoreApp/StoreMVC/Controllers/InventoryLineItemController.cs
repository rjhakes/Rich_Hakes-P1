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
    public class InventoryLineItemController : Controller
    {
        private IInventoryLineItemBL _inventoryLineItemBL;
        private IProductBL _productBL;
        private IMapper _mapper;

        public InventoryLineItemController(IInventoryLineItemBL inventoryLineItemBL, IProductBL productBL, IMapper mapper)
        {
            _inventoryLineItemBL = inventoryLineItemBL;
            _productBL = productBL;
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

        public ActionResult AddToCart()
        {
            return View();
        }
    }
}
