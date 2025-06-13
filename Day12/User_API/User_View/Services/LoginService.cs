namespace User_View.Services
{
    public class LoginService
    {
        private readonly HttpClient _httpClient;

        public LoginService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
        }
    }
}
