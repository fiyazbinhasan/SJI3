using Microsoft.AspNetCore.Mvc;
using SJI3.Infrastructure.AntiCorruption.HttpClients;

namespace SJI3.API.Controllers
{
    [Route("SJI3/api/[controller]")]
    [ApiController]
    public class DemoHttpController : BaseController
    {
        private readonly IDemoClientApi _demoClientApi;

        public DemoHttpController(IDemoClientApi demoClientApi)
        {
            _demoClientApi = demoClientApi;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_demoClientApi.Get());
        }
    }
}
