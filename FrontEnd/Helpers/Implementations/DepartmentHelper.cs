using FrontEnd.ApiModels;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Newtonsoft.Json;

namespace FrontEnd.Helpers.Implementations
{
    public class DepartmentHelper : IDepartmentHelper
    {
        private readonly IServiceHelper _helper;

        public string Token { get; set; }

        public DepartmentHelper(IServiceHelper helper)
        {
            _helper = helper;
        }

        private DepartmentViewModel Convertir(DepartmentAPI department)
        {
            return new DepartmentViewModel
            {
                DepartmentId = department.DepartmentId,
                Name = department.Name,
                Budget = department.Budget,
                StartDate = department.StartDate,
                Administrator = department.Administrator
            };
        }

        private DepartmentAPI Convertir(DepartmentViewModel department)
        {
            return new DepartmentAPI
            {
                DepartmentId = department.DepartmentId,
                Name = department.Name,
                Budget = department.Budget,
                StartDate = department.StartDate,
                Administrator = department.Administrator
            };
        }

        public void Add(DepartmentViewModel department)
        {
            // Configurar token de autorización
            _helper.HttpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);

            var response = _helper.Post("api/department", Convertir(department));

            if (response != null)
            {
                var content = response.Content.ReadAsStringAsync().Result;

                var result = JsonConvert.DeserializeObject<DepartmentAPI>(content);

                if (result != null)
                    Convertir(result);
            }
        }

        public void Update(DepartmentViewModel department)
        {
            // Configurar token de autorización
            _helper.HttpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);

            var response = _helper.Put("api/department", Convertir(department));

            if (response != null)
            {
                var content = response.Content.ReadAsStringAsync().Result;

                var result = JsonConvert.DeserializeObject<DepartmentAPI>(content);

                if (result != null)
                    Convertir(result);
            }
        }

        public void Delete(int id)
        {
            // Configurar token de autorización
            _helper.HttpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);

            _helper.Delete("api/department/" + id);
        }

        public DepartmentViewModel Get(int id)
        {
            // CORREGIDO: Configurar token de autorización antes de la petición
            _helper.HttpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);

            var response = _helper.GetResponseMessage("api/department/" + id);
            var department = new DepartmentViewModel();

            if (response != null)
            {
                var content = response.Content.ReadAsStringAsync().Result;

                var result = JsonConvert.DeserializeObject<DepartmentAPI>(content);

                if (result != null)
                    department = Convertir(result);
            }

            return department;
        }

        public List<DepartmentViewModel> GetDepartments()
        {
            _helper.HttpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);

            var response = _helper.GetResponseMessage("api/department");
            var lista = new List<DepartmentViewModel>();

            if (response != null)
            {
                var content = response.Content.ReadAsStringAsync().Result;

                var departments = JsonConvert.DeserializeObject<List<DepartmentAPI>>(content);

                if (departments != null)
                {
                    foreach (var item in departments)
                    {
                        lista.Add(Convertir(item));
                    }
                }
            }

            return lista;
        }
    }
}