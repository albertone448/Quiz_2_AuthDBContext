using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;
using Microsoft.Extensions.Logging;

namespace BackEnd.Services.Implementations
{
    public class PersonService : IPersonService
    {
        private readonly ILogger<PersonService> _logger;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public PersonService(IUnidadDeTrabajo unidad,
                             ILogger<PersonService> logger)
        {
            _unidadDeTrabajo = unidad;
            _logger = logger;
        }

        private PersonDTO Convertir(Person person)
        {
            return new PersonDTO
            {
                PersonID = person.PersonID,
                LastName = person.LastName,
                FirstName = person.FirstName,
                HireDate = person.HireDate,
                EnrollmentDate = person.EnrollmentDate,
                Discriminator = person.Discriminator
            };
        }

        private Person Convertir(PersonDTO personDTO)
        {
            return new Person
            {
                PersonID = personDTO.PersonID,
                LastName = personDTO.LastName,
                FirstName = personDTO.FirstName,
                HireDate = personDTO.HireDate,
                EnrollmentDate = personDTO.EnrollmentDate,
                Discriminator = personDTO.Discriminator
            };
        }

        public PersonDTO AddPerson(PersonDTO personDTO)
        {
            try
            {
                _logger.LogInformation("Ingresa a AddPerson");
                _unidadDeTrabajo.PersonDAL.Add(Convertir(personDTO));
                _unidadDeTrabajo.Complete();
                return personDTO;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error en AddPerson");
                throw;
            }
        }

        public void DeletePerson(int id)
        {
            try
            {
                _logger.LogInformation($"Intentando eliminar persona con ID: {id}");
                var person = new Person { PersonID = id };
                _unidadDeTrabajo.PersonDAL.Remove(person);
                _unidadDeTrabajo.Complete();
                _logger.LogInformation($"Persona con ID: {id} eliminada exitosamente");
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                _logger.LogError(ex, $"Error al eliminar persona con ID: {id} - Restricción de base de datos");

                // Verificar si es un error de restricción de clave foránea
                if (ex.InnerException?.Message.Contains("REFERENCE constraint") == true ||
                    ex.InnerException?.Message.Contains("FK_") == true)
                {
                    throw new InvalidOperationException("No se puede eliminar esta persona porque tiene registros relacionados en el sistema (calificaciones, cursos, etc.). Primero debe eliminar o reasignar todos los registros dependientes.", ex);
                }

                throw new InvalidOperationException("Error al eliminar la persona. Por favor, contacte al administrador del sistema.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error inesperado al eliminar persona con ID: {id}");
                throw new InvalidOperationException("Error inesperado al eliminar la persona. Por favor, contacte al administrador del sistema.", ex);
            }
        }

        public IEnumerable<PersonDTO> GetPersons()
        {
            var persons = _unidadDeTrabajo.PersonDAL.Get();
            var personDTOs = new List<PersonDTO>();
            foreach (var person in persons)
            {
                personDTOs.Add(Convertir(person));
            }
            return personDTOs;
        }

        public PersonDTO GetPersonById(int id)
        {
            var result = _unidadDeTrabajo.PersonDAL.FindById(id);
            if (result == null)
                return null;
            return Convertir(result);
        }

        public void UpdatePerson(PersonDTO personDTO)
        {
            var entity = Convertir(personDTO);
            _unidadDeTrabajo.PersonDAL.Update(entity);
            _unidadDeTrabajo.Complete();
        }
    }
}