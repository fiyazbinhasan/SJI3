using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using SJI3.API.Common;

namespace SJI3.API.Controllers;

public class BaseController : ControllerBase
{
    protected new IActionResult Ok()
    {
        return base.Ok(Envelope.Ok());
    }

    protected IActionResult Ok<T>(T result)
    {
        return base.Ok(Envelope.Ok(result));
    }

    protected IActionResult Error(string errorMessage)
    {
        return BadRequest(Envelope.Error(errorMessage));
    }

    protected IActionResult FromResult(Result result)
    {
        return result.IsSuccess ? Ok() : Error(result.Error);
    }
}