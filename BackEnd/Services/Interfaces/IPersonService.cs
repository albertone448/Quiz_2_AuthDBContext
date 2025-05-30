using BackEnd.DTO;

namespace BackEnd.Services.Interfaces
{
    public interface IPersonService
    {
        IEnumerable<PersonDTO> GetPersons();
        PersonDTO GetPersonById(int id);
        PersonDTO AddPerson(PersonDTO person);
        void UpdatePerson(PersonDTO person);
        void DeletePerson(int id);
    }
}