using Microsoft.AspNetCore.Mvc;
using WebApplication9.Models.ViewModel;
using WebApplication9.Filters;
using WebApplication9.DAL;
using WebApplication9.Models;

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
            if (ModelState.IsValid)
            {
                UsuarioDAL usuarioDAL = new UsuarioDAL();
                Usuario usuario = usuarioDAL.GetUsuarioLogin(loginViewModel.Username, loginViewModel.Password);

               /* if (loginViewModel.Username == "admin" && loginViewModel.Password == "admin")
                {
                    // Save user session
                    HttpContext.Session.SetString("Username", loginViewModel.Username);
                    return RedirectToAction("Index", "Home");
                }*/

                if(usuario != null)
                {
                    HttpContext.Session.SetString("Username", loginViewModel.Username);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid username or password.");
            }
            return View(loginViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            // Clear the session to log the user out
            HttpContext.Session.Clear();

            // Redirect to the login page after logout
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(SignUpViewModel signUpViewModel)
        {
            if (ModelState.IsValid)
            {
                UsuarioDAL usuarioDAL = new UsuarioDAL();
                Usuario usuarioNuevo = new Usuario();

                usuarioNuevo.UserName = signUpViewModel.UserName;


                Usuario usuarioExistente = usuarioDAL.GetUsuarioLogin(usuarioNuevo.UserName, usuarioNuevo.Password);

                if(usuarioExistente != null)
                {
                    ModelState.AddModelError("", "Usuario Existente");
                    return View(signUpViewModel);
                }


                usuarioDAL.CreateUsuario(usuarioNuevo, signUpViewModel.Password);

                Usuario ValidarCreacion = usuarioDAL.GetUsuarioLogin(signUpViewModel.UserName,signUpViewModel.Password);

                if(ValidarCreacion != null)
                {
                    HttpContext.Session.SetString("Username", usuarioNuevo.UserName);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "No se ha podido crear usuario");
            }

            return View(signUpViewModel);
        }
    }
}
