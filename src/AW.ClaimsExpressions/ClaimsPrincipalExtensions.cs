using System;
using System.Linq;
using System.Security.Claims;

namespace AW.ClaimsExpressions;

public static class ClaimsPrincipalExtensions
{
    public static bool ExistsClaim(ClaimsPrincipal p, string claim)
    {
        if (p == null || p.Claims == null || p.Claims.Any() == false)
            return false;

        return p.Claims.Any(c => string.Equals(c.Type, claim, StringComparison.OrdinalIgnoreCase));
    }

    public static bool AnyClaimThatEquals(ClaimsPrincipal p, string claim, string value)
    {
        if (p == null || p.Claims == null || p.Claims.Any() == false)
            return false;

        return p.Claims.Any(c => string.Equals(c.Type, claim, StringComparison.OrdinalIgnoreCase)
            && c.Value != null && c.Value.Equals(value, StringComparison.OrdinalIgnoreCase));
    }

    public static bool AnyClaimThatContains(ClaimsPrincipal p, string claim, string value)
    {
        if (p == null || p.Claims == null || p.Claims.Any() == false || value == null)
            return false;

        return p.Claims.Any(c => string.Equals(c.Type, claim, StringComparison.OrdinalIgnoreCase)
            && c.Value != null && c.Value.Contains(value, StringComparison.OrdinalIgnoreCase));
    }

    public static bool AnyClaimThatStartsWith(ClaimsPrincipal p, string claim, string value)
    {
        if (p == null || p.Claims == null || p.Claims.Any() == false || value == null)
            return false;

        return p.Claims.Any(c => string.Equals(c.Type, claim, StringComparison.OrdinalIgnoreCase)
            && c.Value != null && c.Value.StartsWith(value, StringComparison.OrdinalIgnoreCase));
    }

    public static bool AnyClaimThatEndsWith(ClaimsPrincipal p, string claim, string value)
    {
        if (p == null || p.Claims == null || p.Claims.Any() == false || value == null)
            return false;

        return p.Claims.Any(c => string.Equals(c.Type, claim, StringComparison.OrdinalIgnoreCase)
            && c.Value != null && c.Value.EndsWith(value, StringComparison.OrdinalIgnoreCase));
    }
}