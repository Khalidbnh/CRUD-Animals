using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication9.DAL;
using WebApplication9.Models;
using WebApplication9.Models.ViewModel;
using WebApplication9.Filters;


namespace WebApplication9.Controllers
{
    [SessionCheck]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            /*if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
            {
                return RedirectToAction("Login", "Authentication");
            }*/

            DAL_Animal dal = new DAL_Animal();
            DAL_TipoAnimal dAL_TipoAnimal = new DAL_TipoAnimal();

            AnimalViewModel animalViewModel = new AnimalViewModel();

            animalViewModel.Animales = dal.GetAll();
            animalViewModel.TipoAnimals = dAL_TipoAnimal.GetAll();
            
            return View(animalViewModel);
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

        [HttpPost]
        public IActionResult AnimalDetails(int id)
        {
            ViewData["Mensaje"] = "Details desde controlador";
            return RedirectToAction("Details", "Animal", new { id });
        }


        


    }
}
