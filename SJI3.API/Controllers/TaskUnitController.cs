using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using SJI3.API.Decorators;
using SJI3.Core.Features.TaskUnit.Exists;
using SJI3.Core.Features.TaskUnit.Get;
using SJI3.Core.Features.TaskUnit.Post;
using SJI3.Core.Features.TaskUnit.Put;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using SJI3.Core.Common.Extensions;

namespace SJI3.API.Controllers;

[Route("SJI3/api/[controller]")]
[ApiController]
public class TaskUnitController : BaseController
{
    private readonly IMediator _mediator;

    public TaskUnitController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("", Name = "GetTaskUnits")]
    [ExposePaginationHeaders]
    public async Task<IActionResult> Get([FromQuery] ResourceParameters resourceParameters)
    {
        var response = await _mediator
            .CreateRequestClient<IGetTaskUnitsQuery>()
            .GetResponse<GetTaskUnitsResponse>(new
            {
                ResourceParameters = resourceParameters
            });

        HttpContext
            .Response
            .Headers
            .Add("X-Pagination", JsonSerializer.Serialize(new
            {
                response.Message.PaginationMetadata.TotalCount,
                response.Message.PaginationMetadata.PageSize,
                response.Message.PaginationMetadata.TotalPages,
                response.Message.PaginationMetadata.CurrentPage,
                response.Message.PaginationMetadata.HasNext,
                response.Message.PaginationMetadata.HasPrevious
            },
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }));

        return Ok(response.Message.TaskUnits.ShapeData(resourceParameters.Fields));
    }

    [Authorize("SJI3ApiPolicy")]
    [HttpPost("", Name = "PostTaskUnit")]
    public async Task<IActionResult> Post([FromBody] PostTaskUnitCommand taskUnit)
    {
        var response = await _mediator
            .CreateRequestClient<PostTaskUnitCommand>()
            .GetResponse<IPostTaskUnitResponse>(taskUnit);

        return Ok(response.Message.IsAdded);
    }

    [Authorize("SJI3ApiPolicy")]
    [HttpPut("{id:guid}/UpdateTaskUnitStatus")]
    public async Task<IActionResult> UpdateTaskUnitStatus(Guid id, [FromBody] PutTaskUnitCommand taskUnit)
    {
        var exists = await _mediator
            .CreateRequestClient<ITaskUnitExistsQuery>()
            .GetResponse<TaskUnitExistsResponse>(new
            {
                Id = id
            });

        if (!exists.Message.Exists)
            return Error($"Task Unit doesn't exists with id {id}");

        var response = await _mediator
            .CreateRequestClient<PutTaskUnitCommand>()
            .GetResponse<IPutTaskUnitResponse>(taskUnit);

        return Ok(response.Message.IsUpdated);
    }
}