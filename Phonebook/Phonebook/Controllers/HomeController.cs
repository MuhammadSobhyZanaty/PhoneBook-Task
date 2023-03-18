using Microsoft.AspNetCore.Mvc;
using Phonebook.Core;
using Phonebook.Models;
using System.Diagnostics;

namespace Phonebook.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IConfiguration Configuration;
        public HomeController(ILogger<HomeController> logger, IConfiguration Config)
        {
            this.Configuration = Config;
            _logger = logger;
        }

        public IActionResult Index()
        {
            DatabaseOperations databaseOperations = new DatabaseOperations(this.Configuration);
            return View("Index",databaseOperations.GetContact());
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}        
        public IActionResult Create()
        {
            return View("Create");
        }        
        public IActionResult CreateNumber(int id)
        {
            TempData["id"] = id;
            TempData.Keep();
            return View("CreateNumber");
        }
        public IActionResult AddContact(Contact Obj)
        {
            Services services = new Services(this.Configuration);
            services.AddContact(Obj);
            return RedirectToAction("Index");
        }        
        public IActionResult AddNumber(Numbers Obj)
        {
            Obj.ContactID = Convert.ToInt32(TempData["id"].ToString());
            Services services = new Services(this.Configuration);
            services.AddNumber(Obj);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}