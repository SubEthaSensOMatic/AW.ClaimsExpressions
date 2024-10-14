using Microsoft.AspNetCore.Authorization;

namespace AW.ClaimsExpressions.AspNetCore;

public class AuthorizeByClaimsExpressionAttribute : AuthorizeAttribute
{
    public string ConfigKey
    {
        get => Policy?[ClaimsExpressionPolicyProvider.POLICY_PREFIX.Length..] ?? string.Empty;
        set => Policy = $"{ClaimsExpressionPolicyProvider.POLICY_PREFIX}{value}";
    }

    public AuthorizeByClaimsExpressionAttribute(string configKey)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(configKey, nameof(configKey));
        ConfigKey = configKey;
    }
}
