using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace AW.ClaimsExpressions.AspNetCore;

public static class ClaimsExpressionsConfiguration
{
    public static IServiceCollection AddClaimsExpressions(this IServiceCollection @this)
    {
        @this.AddSingleton<IAuthorizeByClaimsExpression, AuthorizeByClaimsExpression>();
        @this.AddSingleton<IAuthorizationHandler, ClaimsExpressionHandler>();
        @this.AddSingleton<IAuthorizationPolicyProvider, ClaimsExpressionPolicyProvider>();
        return @this;
    }
}
