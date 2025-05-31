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
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        // GET: api/Person
        [HttpGet]
        public IEnumerable<PersonDTO> Get()
        {
            return _personService.GetPersons();
        }

        // GET: api/Person/5
        [HttpGet("{id}")]
        public ActionResult<PersonDTO> Get(int id)
        {
            var person = _personService.GetPersonById(id);
            if (person == null)
                return NotFound();
            return person;
        }

        // POST: api/Person
        [HttpPost]
        public ActionResult<PersonDTO> Post([FromBody] PersonDTO person)
        {
            var created = _personService.AddPerson(person);
            return CreatedAtAction(nameof(Get), new { id = created.PersonID }, created);
        }

        // PUT: api/Person
        [HttpPut]
        public void Put([FromBody] PersonDTO person)
        {
            _personService.UpdatePerson(person);
        }

        // DELETE: api/Person/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _personService.DeletePerson(id);
                return NoContent(); // 204 - Eliminación exitosa
            }
            catch (InvalidOperationException ex)
            {
                // Error de restricción de clave foránea o error controlado
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Error inesperado
                return StatusCode(500, new { message = "Error interno del servidor al eliminar la persona." });
            }
        }
    }
}