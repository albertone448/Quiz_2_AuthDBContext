using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    [Authorize]
    public class PersonController : Controller
    {
        private readonly IPersonHelper _personHelper;

        public PersonController(IPersonHelper personHelper)
        {
            _personHelper = personHelper;
        }

        private string GetToken()
        {
            return HttpContext.Session.GetString("Token");
        }

        private bool SetTokenAndValidate()
        {
            var token = GetToken();
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }
            _personHelper.Token = token;
            return true;
        }

        private bool IsValidSqlDateTime(DateTime? date)
        {
            if (!date.HasValue) return true;
            return date.Value >= new DateTime(1753, 1, 1) && date.Value <= new DateTime(9999, 12, 31);
        }

        // GET: PersonController
        public ActionResult Index()
        {
            if (!SetTokenAndValidate())
            {
                return RedirectToAction("Login", "Login");
            }

            var persons = _personHelper.GetPersons();
            return View(persons);
        }

        // GET: PersonController/Details/5
        public ActionResult Details(int id)
        {
            if (!SetTokenAndValidate())
            {
                return RedirectToAction("Login", "Login");
            }

            var person = _personHelper.Get(id);
            return View(person);
        }

        // GET: PersonController/Create
        public ActionResult Create()
        {
            var model = new PersonViewModel();
            return View(model);
        }

        // POST: PersonController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PersonViewModel person)
        {
            try
            {
                if (!SetTokenAndValidate())
                {
                    return RedirectToAction("Login", "Login");
                }

                // Validar fechas antes de guardar
                if (!IsValidSqlDateTime(person.HireDate))
                {
                    ModelState.AddModelError("HireDate", "La fecha de contratación debe estar entre 1/1/1753 y 12/31/9999");
                    return View(person);
                }

                if (!IsValidSqlDateTime(person.EnrollmentDate))
                {
                    ModelState.AddModelError("EnrollmentDate", "La fecha de inscripción debe estar entre 1/1/1753 y 12/31/9999");
                    return View(person);
                }

                if (ModelState.IsValid)
                {
                    _personHelper.Add(person);
                    return RedirectToAction(nameof(Index));
                }

                return View(person);
            }
            catch (Exception ex)
            {
                // Log del error real para debugging
                ModelState.AddModelError("", "Error al crear la persona: " + ex.Message);
                return View(person);
            }
        }

        // GET: PersonController/Edit/5
        public ActionResult Edit(int id)
        {
            if (!SetTokenAndValidate())
            {
                return RedirectToAction("Login", "Login");
            }

            var person = _personHelper.Get(id);
            return View(person);
        }

        // POST: PersonController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PersonViewModel person)
        {
            try
            {
                if (!SetTokenAndValidate())
                {
                    return RedirectToAction("Login", "Login");
                }

                // Validar fechas antes de actualizar
                if (!IsValidSqlDateTime(person.HireDate))
                {
                    ModelState.AddModelError("HireDate", "La fecha de contratación debe estar entre 1/1/1753 y 12/31/9999");
                    return View(person);
                }

                if (!IsValidSqlDateTime(person.EnrollmentDate))
                {
                    ModelState.AddModelError("EnrollmentDate", "La fecha de inscripción debe estar entre 1/1/1753 y 12/31/9999");
                    return View(person);
                }

                if (ModelState.IsValid)
                {
                    _personHelper.Update(person);
                    return RedirectToAction(nameof(Index));
                }

                return View(person);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al actualizar la persona: " + ex.Message);
                return View(person);
            }
        }

        // GET: PersonController/Delete/5
        public ActionResult Delete(int id)
        {
            if (!SetTokenAndValidate())
            {
                return RedirectToAction("Login", "Login");
            }

            var person = _personHelper.Get(id);
            return View(person);
        }

        // POST: PersonController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                if (!SetTokenAndValidate())
                {
                    return RedirectToAction("Login", "Login");
                }

                _personHelper.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (!SetTokenAndValidate())
                {
                    return RedirectToAction("Login", "Login");
                }

                var person = _personHelper.Get(id);
                ModelState.AddModelError("", "Error al eliminar la persona: " + ex.Message);
                return View(person);
            }
        }
    }
}