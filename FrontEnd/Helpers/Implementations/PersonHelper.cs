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

        public bool Delete(int id)
        {
            try
            {
                // Configurar token de autorización
                _helper.HttpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);

                var response = _helper.Delete("api/person/" + id);

                if (response != null && response.IsSuccessStatusCode)
                {
                    return true;
                }
                else if (response != null && response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    // Leer el mensaje de error del servidor
                    var content = response.Content.ReadAsStringAsync().Result;
                    var errorResponse = JsonConvert.DeserializeObject<dynamic>(content);
                    string errorMessage = errorResponse?.message ?? "Error al eliminar la persona";

                    throw new InvalidOperationException(errorMessage);
                }
                else
                {
                    throw new InvalidOperationException("Error al comunicarse con el servidor para eliminar la persona");
                }
            }
            catch (InvalidOperationException)
            {
                // Re-lanzar excepciones de negocio
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error inesperado al eliminar la persona: " + ex.Message, ex);
            }
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