using BackEnd.DTO;
using BackEnd.Services.Implementations;
using BackEnd.Services.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        // GET: api/Department
        [HttpGet]
        public IEnumerable<DepartmentDTO> Get()
        {
            return _departmentService.GetDepartments();
        }

        // GET: api/Department/5
        [HttpGet("{id}")]
        public ActionResult<DepartmentDTO> Get(int id)
        {
            var department = _departmentService.GetDepartmentById(id);
            if (department == null)
                return NotFound();

            return department;
        }

        // POST: api/Department
        [HttpPost]
        public ActionResult<DepartmentDTO> Post([FromBody] DepartmentDTO department)
        {
            var created = _departmentService.AddDepartment(department);
            return CreatedAtAction(nameof(Get), new { id = created.DepartmentId }, created);
        }

        // PUT api/<CategoryController>/5
        [HttpPut]
        public void Put([FromBody] DepartmentDTO department)
        {
            _departmentService.UpdateDepartment(department);
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _departmentService.DeleteDepartment(id);
        }
    }
}

