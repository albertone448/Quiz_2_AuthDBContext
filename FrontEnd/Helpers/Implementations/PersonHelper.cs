using FrontEnd.ApiModels;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Newtonsoft.Json;

namespace FrontEnd.Helpers.Implementations
{
    public class PersonHelper : IPersonHelper
    {
        private readonly IServiceHelper _helper;

        public string Token { get; set; }

        public PersonHelper(IServiceHelper helper)
        {
            _helper = helper;
        }

        private PersonViewModel Convertir(PersonAPI person)
        {
            return new PersonViewModel
            {
                PersonID = person.PersonID,
                LastName = person.LastName,
                FirstName = person.FirstName,
                HireDate = person.HireDate,
                EnrollmentDate = person.EnrollmentDate,
                Discriminator = person.Discriminator
            };
        }

        private PersonAPI Convertir(PersonViewModel person)
        {
            return new PersonAPI
            {
                PersonID = person.PersonID,
                LastName = person.LastName,
                FirstName = person.FirstName,
                HireDate = person.HireDate,
                EnrollmentDate = person.EnrollmentDate,
                Discriminator = person.Discriminator
            };
        }

        public void Add(PersonViewModel person)
        {
            // Configurar token de autorización
            _helper.HttpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);

            var response = _helper.Post("api/person", Convertir(person));

            if (response != null)
            {
                var content = response.Content.ReadAsStringAsync().Result;

                var result = JsonConvert.DeserializeObject<PersonAPI>(content);

                if (result != null)
                    Convertir(result);
            }
        }

        public void Update(PersonViewModel person)
        {
            // Configurar token de autorización
            _helper.HttpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);

            var response = _helper.Put("api/person", Convertir(person));

            if (response != null)
            {
                var content = response.Content.ReadAsStringAsync().Result;

                var result = JsonConvert.DeserializeObject<PersonAPI>(content);

                if (result != null)
                    Convertir(result);
            }
        }

        public void Delete(int id)
        {
            // Configurar token de autorización
            _helper.HttpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);

            _helper.Delete("api/person/" + id);
        }

        public PersonViewModel Get(int id)
        {
            // CORREGIDO: Configurar token de autorización antes de la petición
            _helper.HttpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);

            var response = _helper.GetResponseMessage("api/person/" + id);
            var person = new PersonViewModel();

            if (response != null)
            {
                var content = response.Content.ReadAsStringAsync().Result;

                var result = JsonConvert.DeserializeObject<PersonAPI>(content);

                if (result != null)
                    person = Convertir(result);
            }

            return person;
        }

        public List<PersonViewModel> GetPersons()
        {
            _helper.HttpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);

            var response = _helper.GetResponseMessage("api/person");
            var lista = new List<PersonViewModel>();

            if (response != null)
            {
                var content = response.Content.ReadAsStringAsync().Result;

                var persons = JsonConvert.DeserializeObject<List<PersonAPI>>(content);

                if (persons != null)
                {
                    foreach (var item in persons)
                    {
                        lista.Add(Convertir(item));
                    }
                }
            }

            return lista;
        }
    }
}