using Microsoft.AspNetCore.Mvc;

namespace User_View.Services
{
    public class MissionService : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IHttpClientFactory _clientFactory;

        public MissionService(IHttpClientFactory clientFactory, IHttpContextAccessor contextAccessor)
        {
            _clientFactory = clientFactory;
            _contextAccessor = contextAccessor;
        }


    }
}
