using Microsoft.AspNetCore.Authorization;
using OpenIddict.Abstractions;

namespace SJI3.API.Common;

public class RequireScopeHandler: AuthorizationHandler<RequireScope>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, RequireScope requirement)
    {
        if (context == null)
            throw new ArgumentNullException(nameof(context));
        if (requirement == null)
            throw new ArgumentNullException(nameof(requirement));

        var scopeClaim =  context.User.Claims.FirstOrDefault(t => t.Type == "scope");


        if (scopeClaim != null && context.User.HasScope("sji3_api"))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}