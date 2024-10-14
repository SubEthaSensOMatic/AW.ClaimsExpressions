using Microsoft.AspNetCore.Authorization;

namespace AW.ClaimsExpressions.AspNetCore;

internal class ClaimsExpressionHandler : AuthorizationHandler<ClaimsExpressionRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimsExpressionRequirement requirement)
    {
        if (requirement.Validator(context.User))
            context.Succeed(requirement);
        return Task.CompletedTask;
    }
}
