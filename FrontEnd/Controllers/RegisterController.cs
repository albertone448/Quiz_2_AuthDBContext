using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IRegisterHelper _registerHelper;

        public RegisterController(IRegisterHelper registerHelper)
        {
            _registerHelper = registerHelper;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var registroExitoso = _registerHelper.Register(model.UserName, model.Password, model.Email);

                    if (registroExitoso)
                    {
                        TempData["RegistroExitoso"] = "¡Registro exitoso! Ahora puedes iniciar sesión.";
                        // Registro exitoso, redirige al login
                        return RedirectToAction("Login", "Login");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Error al registrar. El usuario o email ya existe, o verifique los datos.");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Error interno al registrar. Intente más tarde.");
                }
            }

            return View(model);
        }
    }
}