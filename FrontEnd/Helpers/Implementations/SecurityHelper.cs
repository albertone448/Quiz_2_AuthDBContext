using FrontEnd.ApiModels;
using FrontEnd.Helpers.Interfaces;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;

namespace FrontEnd.Helpers.Implementations
{
    public class SecurityHelper : ISecurityHelper
    {
        IServiceHelper _serviceHelper;

        public SecurityHelper(IServiceHelper serviceHelper)
        {
            _serviceHelper = serviceHelper;
        }

        public LoginAPI Login(string userName, string password)
        {
            try
            {

                var response = _serviceHelper
                            .Post("api/Auth/login",
                                new { UserName = userName, Password = password });

                var content = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<LoginAPI>(content);
                }
                else
                {
                    return new LoginAPI();
                    { }
                }



            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
