using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using StoreMVC.Models;
using WebErrorLogging.Utilities;

namespace StoreMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        readonly IDiagnosticContext _diagnosticContext;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /*public HomeController(IDiagnosticContext diagnosticContext)
        {
            _diagnosticContext = diagnosticContext ??
                throw new ArgumentNullException(nameof(diagnosticContext));
        }*/

        public IActionResult Index()
        {
            //_diagnosticContext.Set("CatalogLoadTime", 1432);
            try
            {
                /*Helper.WriteInformation("Information");
                Helper.WriteDebug(null, "Debug ");
                Helper.WriteWarning(null, "Warning ");*/
                //throw new NotImplementedException();
                return View();
            }
            catch (Exception e)
            {
                Helper.WriteError(e, "Error");
                Helper.WriteFatal(e, "Fatal");
                Helper.WriteVerbose(e, "Verbose");
                throw new NotImplementedException();
                
            }
            finally
            {
                
            }

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
