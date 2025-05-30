namespace FrontEnd.ApiModels
{
    public class LoginAPI
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public TokenAPI? Token { get; set; }

        public List<string>? Roles { get; set; }
    }
}
