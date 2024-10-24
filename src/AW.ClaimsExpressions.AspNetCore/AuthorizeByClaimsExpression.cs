using Microsoft.Extensions.Configuration;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AW.ClaimsExpressions.AspNetCore;

internal class AuthorizeByClaimsExpression : IAuthorizeByClaimsExpression
{
    private readonly IConfiguration _configuration;

    public AuthorizeByClaimsExpression(IConfiguration configuration)
        => _configuration = configuration;

    public async Task<bool> IsAuthorized(ClaimsPrincipal user, string configurationKey)
    {
        ArgumentNullException.ThrowIfNull(nameof(user));
        ArgumentException.ThrowIfNullOrWhiteSpace(configurationKey, nameof(configurationKey));

        var expression = _configuration.GetValue<string>(configurationKey);
        if (string.IsNullOrWhiteSpace(expression))
            throw new InvalidOperationException($"Missing claims expression for config key '{configurationKey}'.");

        var validator = await Compiler.Compile(expression);

        return validator(user);
    }
}
