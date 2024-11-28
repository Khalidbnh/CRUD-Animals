using Microsoft.AspNetCore.Mvc;
using WebApplication9.Models.ViewModel;

namespace WebApplication9.Controllers
{
    public class AuthenticationController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if(ModelState.IsValid)
            {
                //Validar Usuario
                if(loginViewModel.Username == "admin" && loginViewModel.Password == "admin")
                {//authentificacion exitosa

                    HttpContext.Session.SetString("Username", loginViewModel.Username);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Usuario o contraseña incorrectos");
            }
            return View(loginViewModel);
        }

    }
}
