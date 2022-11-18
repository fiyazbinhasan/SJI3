using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using SJI3.Core.Features.Menu.AddUserToMenu;
using SJI3.Core.Features.Menu.Get;
using SJI3.Core.Features.Menu.Post;

namespace SJI3.API.Controllers;

[Route("SJI3/api/[controller]")]
[ApiController]
public class MenuController : BaseController
{
    private readonly IMediator _mediator;

    public MenuController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("", Name = "GetMenus")]
    public async Task<IActionResult> Get()
    {
        var response = await _mediator
            .CreateRequestClient<IGetMenusQuery>()
            .GetResponse<GetMenusResponse>(new {});

        return Ok(response.Message.Menus);
    }

    [HttpPost("", Name = "PostMenu")]
    public async Task<IActionResult> Post([FromBody] PostMenuCommand command)
    {
        var response = await _mediator
            .CreateRequestClient<PostMenuCommand>()
            .GetResponse<IPostMenuResponse>(command);

        return Ok(response.Message.IsAdded);
    }
    
    [HttpPost("AddUserToMenu", Name = "AddUserToMenu")]
    public async Task<IActionResult> Post([FromBody] AddUserToMenuCommand command)
    {
        var response = await _mediator
            .CreateRequestClient<AddUserToMenuCommand>()
            .GetResponse<IAddUserToMenuResponse>(command);

        return Ok(response.Message.IsAdded);
    }
}