using FrontEnd.Models;

namespace FrontEnd.Helpers.Interfaces
{
    public interface IPersonHelper
    {
        PersonViewModel Get(int id);
        string Token { get; set; }
        List<PersonViewModel> GetPersons();
        void Add(PersonViewModel person);
        void Update(PersonViewModel person);
        bool Delete(int id);
    }
}