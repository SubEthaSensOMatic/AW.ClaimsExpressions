using Microsoft.AspNetCore.Authorization;

namespace AW.ClaimsExpressions.AspNetCore;

internal class ClaimsExpressionRequirement : IAuthorizationRequirement
{
    public ClaimsExpressionRequirement(ClaimsExpressionValidator validator) => Validator = validator;

    public ClaimsExpressionValidator Validator { get; }
}
