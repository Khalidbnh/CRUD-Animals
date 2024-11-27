using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication9.DAL;
using WebApplication9.Models;
using WebApplication9.Models.ViewModel;


namespace WebApplication9.Controllers
{
    public class AnimalController : Controller
    {
        private readonly DAL_Animal _DAL_Animal = new DAL_Animal();
        private readonly DAL_TipoAnimal _DAL_TipoAnimal = new DAL_TipoAnimal();
        DetailAnimalViewModel _DetailAnimalViewModel = new DetailAnimalViewModel();

        [HttpGet]
        public ActionResult Details(int id)
        {
            _DetailAnimalViewModel.AnimalDetail = _DAL_Animal.GetById(id);

            if(_DetailAnimalViewModel.AnimalDetail == null)
            {
                return NotFound();
            }

            return View(_DetailAnimalViewModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.TipoAnimales = _DAL_TipoAnimal.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Insert(AnimalModel animal)
        {
            if (animal == null)
            {
                return BadRequest("Animal data is required.");
            }

            try
            {
                _DAL_Animal.Insert(animal);
                return RedirectToAction("Index", "Home"); // Redirect back to the list after successful insertion
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // GET: Load the edit form with the existing animal data
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var animal = _DAL_Animal.GetById(id);
            if (animal == null)
            {
                return NotFound();
            }

            ViewBag.TipoAnimales = _DAL_TipoAnimal.GetAll(); // Fill dropdown
            return View(animal);
        }



        // POST: Save the updated animal data
        [HttpPost]
        public IActionResult Edit(AnimalModel animal)
        {
            /*if (!ModelState.IsValid)
            {
                // Reload dropdown in case of an error
                ViewBag.TipoAnimales = _DAL_TipoAnimal.GetAll();
                return View(animal);
            }*/

            try
            {
                _DAL_Animal.Update(animal);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                // Handle error
                ModelState.AddModelError("", $"Error updating record: {ex.Message}");
                ViewBag.TipoAnimales = _DAL_TipoAnimal.GetAll();
                return View(animal);
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _DAL_Animal.Delete(id);
            return RedirectToAction("Index", "Home");
        }

    }
}
