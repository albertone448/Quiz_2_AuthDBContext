using FrontEnd.ApiModels;
using FrontEnd.Helpers.Interfaces;
using Newtonsoft.Json;
using System;

namespace FrontEnd.Helpers.Implementations
{
    public class RegisterHelper : IRegisterHelper
    {
        private readonly IServiceHelper _serviceHelper;

        public RegisterHelper(IServiceHelper serviceHelper)
        {
            _serviceHelper = serviceHelper;
        }

        public bool Register(string userName, string password, string email)
        {
            try
            {
                var response = _serviceHelper
                    .Post("api/Auth/register", new
                    {
                        UserName = userName,
                        Password = password,
                        Email = email
                    });

                // Retorna true si el status code es exitoso (200, 201, etc.)
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false; // Si hay excepción, el registro falló
            }
        }
    }
}