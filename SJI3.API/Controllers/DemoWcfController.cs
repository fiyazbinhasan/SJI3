using Microsoft.AspNetCore.Mvc;
using SJI3.Infrastructure.AntiCorruption.WCFClients.Services;

namespace SJI3.API.Controllers;

[Route("SJI3/api/[controller]")]
[ApiController]
public class DemoWcfController : BaseController
{
    private readonly IService _service;

    public DemoWcfController(IService service)
    {
        _service = service;
    }
        
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        return Ok(_service.GetData(id));
    }
}