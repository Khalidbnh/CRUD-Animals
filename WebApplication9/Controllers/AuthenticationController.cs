﻿using Microsoft.AspNetCore.Mvc;
using WebApplication9.Models.ViewModel;
using WebApplication9.Filters;

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
                if (loginViewModel.Username == "admin" && loginViewModel.Password == "admin")
                {
                    // Save user session
                    HttpContext.Session.SetString("Username", loginViewModel.Username);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid username or password.");
            }
            return View(loginViewModel);
        }

    }
}
