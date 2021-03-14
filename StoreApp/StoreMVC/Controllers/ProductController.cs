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
    public class ProductController : Controller
    {
        private IProductBL _productBL;
        private IMapper _mapper;

        public ProductController(IProductBL productBL, IMapper mapper)
        {
            _productBL = productBL;
            _mapper = mapper;
        }
        // GET: ProductController
        public ActionResult Index()
        {
            return View(_productBL.GetProducts().Select(x => _mapper.cast2ProductIndexVM(x)).ToList());
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View(_mapper.cast2ProductIndexVM(_productBL.GetProductById(id)));
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCRVM newProduct)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _productBL.AddProduct(_mapper.cast2Product(newProduct));
                    //Helper.WriteInformation($"Product created-- Email: {newProduct.ProductEmail}");
                    Log.Information($"Product created-- Name: {newProduct.ProdName}");
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

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_mapper.cast2ProductEditVM(_productBL.GetProductById(id)));
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductEditVM product2BUpdated)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    _productBL.UpdateProduct(_mapper.cast2Product(product2BUpdated));
                    Log.Information($"Product updated-- Name: {product2BUpdated.ProdName}");
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

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                _productBL.DeleteProduct(_productBL.GetProductById(id));
                Log.Information($"Product deleted-- ID: {id}");
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

        // POST: ProductController/Delete/5
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
