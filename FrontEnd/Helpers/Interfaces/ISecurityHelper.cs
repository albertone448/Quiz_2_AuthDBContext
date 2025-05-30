using FrontEnd.ApiModels;

namespace FrontEnd.Helpers.Interfaces
{
    public interface ISecurityHelper
    {

        LoginAPI Login(string userName, string password);
    }
}
