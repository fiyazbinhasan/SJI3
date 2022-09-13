using Microsoft.AspNetCore.Mvc.Filters;

namespace SJI3.API.Decorators;

public class ExposePaginationHeadersAttribute : ResultFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        context.HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "X-Pagination");
        base.OnResultExecuting(context);
    }
}