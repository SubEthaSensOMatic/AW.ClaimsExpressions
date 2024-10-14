using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AW.ClaimsExpressions.AspNetCore;

internal class ClaimsExpressionPolicyProvider : IAuthorizationPolicyProvider
{
    internal const string POLICY_PREFIX = "ClaimsExpression->";
    private const string JWT_AUTHENTICATION_SCHEME = "Bearer";

    private readonly DefaultAuthorizationPolicyProvider _defaultAuthorizationPolicyProvider;
    private readonly IConfiguration _configuration;

    public ClaimsExpressionPolicyProvider(
        IConfiguration configuration,
        IOptions<AuthorizationOptions> authorizationOptions)
    {
        _defaultAuthorizationPolicyProvider
            = new DefaultAuthorizationPolicyProvider(authorizationOptions);
        _configuration = configuration;
    }

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        => _defaultAuthorizationPolicyProvider.GetDefaultPolicyAsync();

    public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
        => _defaultAuthorizationPolicyProvider.GetFallbackPolicyAsync();

    public async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        if (policyName.StartsWith(POLICY_PREFIX) && policyName.Length > POLICY_PREFIX.Length)
        {
            var configKey = policyName[POLICY_PREFIX.Length..];

            if (string.IsNullOrWhiteSpace(configKey))
                throw new InvalidOperationException("Missing config key for claims expression.");

            var expression = _configuration.GetValue<string>(configKey);
            if (string.IsNullOrWhiteSpace(expression))
                throw new InvalidOperationException($"Missing claims expression for config key '{configKey}'.");

            var validator = await Compiler.Compile(expression);
            var req = new ClaimsExpressionRequirement(validator);
            var policy = new AuthorizationPolicyBuilder(JWT_AUTHENTICATION_SCHEME);

            policy.AddRequirements(req);
           
            return policy.Build();
        }
        else
        {
            return await _defaultAuthorizationPolicyProvider.GetPolicyAsync(policyName);
        }
    }
}
