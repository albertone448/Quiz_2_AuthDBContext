using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentHelper _departmentHelper;

        public DepartmentController(IDepartmentHelper departmentHelper)
        {
            _departmentHelper = departmentHelper;
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
            _departmentHelper.Token = token;
            return true;
        }

        private bool IsValidSqlDateTime(DateTime date)
        {
            return date >= new DateTime(1753, 1, 1) && date <= new DateTime(9999, 12, 31);
        }

        // GET: DepartmentController
        public ActionResult Index()
        {
            if (!SetTokenAndValidate())
            {
                return RedirectToAction("Login", "Login");
            }

            var departments = _departmentHelper.GetDepartments();
            return View(departments);
        }

        // GET: DepartmentController/Details/5
        public ActionResult Details(int id)
        {
            if (!SetTokenAndValidate())
            {
                return RedirectToAction("Login", "Login");
            }

            var department = _departmentHelper.Get(id);
            return View(department);
        }

        // GET: DepartmentController/Create
        public ActionResult Create()
        {
            var model = new DepartmentViewModel
            {
                StartDate = DateTime.Today // Establecer la fecha actual
            };
            return View(model);
        }

        // POST: DepartmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DepartmentViewModel department)
        {
            try
            {
                if (!SetTokenAndValidate())
                {
                    return RedirectToAction("Login", "Login");
                }

                // Validar fecha antes de guardar
                if (!IsValidSqlDateTime(department.StartDate))
                {
                    ModelState.AddModelError("StartDate", "La fecha debe estar entre 1/1/1753 y 12/31/9999");
                    return View(department);
                }

                if (ModelState.IsValid)
                {
                    _departmentHelper.Add(department);
                    return RedirectToAction(nameof(Index));
                }

                return View(department);
            }
            catch (Exception ex)
            {
                // Log del error real para debugging
                ModelState.AddModelError("", "Error al crear el departamento: " + ex.Message);
                return View(department);
            }
        }

        // GET: DepartmentController/Edit/5
        public ActionResult Edit(int id)
        {
            if (!SetTokenAndValidate())
            {
                return RedirectToAction("Login", "Login");
            }

            var department = _departmentHelper.Get(id);
            return View(department);
        }

        // POST: DepartmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DepartmentViewModel department)
        {
            try
            {
                if (!SetTokenAndValidate())
                {
                    return RedirectToAction("Login", "Login");
                }

                // Validar fecha antes de actualizar
                if (!IsValidSqlDateTime(department.StartDate))
                {
                    ModelState.AddModelError("StartDate", "La fecha debe estar entre 1/1/1753 y 12/31/9999");
                    return View(department);
                }

                if (ModelState.IsValid)
                {
                    _departmentHelper.Update(department);
                    return RedirectToAction(nameof(Index));
                }

                return View(department);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al actualizar el departamento: " + ex.Message);
                return View(department);
            }
        }

        // GET: DepartmentController/Delete/5
        public ActionResult Delete(int id)
        {
            if (!SetTokenAndValidate())
            {
                return RedirectToAction("Login", "Login");
            }

            var department = _departmentHelper.Get(id);
            return View(department);
        }

        // POST: DepartmentController/Delete/5
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

                _departmentHelper.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (!SetTokenAndValidate())
                {
                    return RedirectToAction("Login", "Login");
                }

                var department = _departmentHelper.Get(id);
                ModelState.AddModelError("", "Error al eliminar el departamento: " + ex.Message);
                return View(department);
            }
        }
    }
}