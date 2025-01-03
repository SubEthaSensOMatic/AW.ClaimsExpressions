using System;
using System.Linq;
using System.Security.Claims;

namespace AW.ClaimsExpressions;

public static class ClaimsPrincipalExtensions
{
    public static bool IsAuthenticated(ClaimsPrincipal p)
        => p.Identities != null && p.Identities.Any() && p.Identities.All(i => i.IsAuthenticated);

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

    public static bool AnyClaimThatIsGeaterThan(ClaimsPrincipal p, string claim, string value)
    {
        if (p == null || p.Claims == null || p.Claims.Any() == false)
            return false;

        return p.Claims.Any(c => string.Equals(c.Type, claim, StringComparison.OrdinalIgnoreCase)
            && c.Value != null && c.Value.CompareTo(value) > 0);
    }

    public static bool AnyClaimThatIsGeaterThanOrEqual(ClaimsPrincipal p, string claim, string value)
    {
        if (p == null || p.Claims == null || p.Claims.Any() == false)
            return false;

        return p.Claims.Any(c => string.Equals(c.Type, claim, StringComparison.OrdinalIgnoreCase)
            && c.Value != null && c.Value.CompareTo(value) >= 0);
    }

    public static bool AnyClaimThatIsLessThan(ClaimsPrincipal p, string claim, string value)
    {
        if (p == null || p.Claims == null || p.Claims.Any() == false)
            return false;

        return p.Claims.Any(c => string.Equals(c.Type, claim, StringComparison.OrdinalIgnoreCase)
            && c.Value != null && c.Value.CompareTo(value) < 0);
    }

    public static bool AnyClaimThatIsLessThanOrEqual(ClaimsPrincipal p, string claim, string value)
    {
        if (p == null || p.Claims == null || p.Claims.Any() == false)
            return false;

        return p.Claims.Any(c => string.Equals(c.Type, claim, StringComparison.OrdinalIgnoreCase)
            && c.Value != null && c.Value.CompareTo(value) <= 0);
    }

    public static bool AnyClaimThatEquals(ClaimsPrincipal p, string claim, long value)
    {
        if (p == null || p.Claims == null || p.Claims.Any() == false)
            return false;

        return p.Claims.Any(c => string.Equals(c.Type, claim, StringComparison.OrdinalIgnoreCase)
            && c.Value != null && long.TryParse(c.Value, out var parsed) && parsed == value);
    }

    public static bool AnyClaimThatIsGeaterThan(ClaimsPrincipal p, string claim, long value)
    {
        if (p == null || p.Claims == null || p.Claims.Any() == false)
            return false;

        return p.Claims.Any(c => string.Equals(c.Type, claim, StringComparison.OrdinalIgnoreCase)
            && c.Value != null && long.TryParse(c.Value, out var parsed) && parsed > value);
    }

    public static bool AnyClaimThatIsGeaterThanOrEqual(ClaimsPrincipal p, string claim, long value)
    {
        if (p == null || p.Claims == null || p.Claims.Any() == false)
            return false;

        return p.Claims.Any(c => string.Equals(c.Type, claim, StringComparison.OrdinalIgnoreCase)
            && c.Value != null && long.TryParse(c.Value, out var parsed) && parsed >= value);
    }

    public static bool AnyClaimThatIsLessThan(ClaimsPrincipal p, string claim, long value)
    {
        if (p == null || p.Claims == null || p.Claims.Any() == false)
            return false;

        return p.Claims.Any(c => string.Equals(c.Type, claim, StringComparison.OrdinalIgnoreCase)
            && c.Value != null && long.TryParse(c.Value, out var parsed) && parsed < value);
    }

    public static bool AnyClaimThatIsLessThanOrEqual(ClaimsPrincipal p, string claim, long value)
    {
        if (p == null || p.Claims == null || p.Claims.Any() == false)
            return false;

        return p.Claims.Any(c => string.Equals(c.Type, claim, StringComparison.OrdinalIgnoreCase)
            && c.Value != null && long.TryParse(c.Value, out var parsed) && parsed <= value);
    }

    public static bool AnyClaimThatEquals(ClaimsPrincipal p, string claim, double value)
    {
        if (p == null || p.Claims == null || p.Claims.Any() == false)
            return false;

        return p.Claims.Any(c => string.Equals(c.Type, claim, StringComparison.OrdinalIgnoreCase)
            && c.Value != null && double.TryParse(c.Value, out var parsed) && parsed == value);
    }

    public static bool AnyClaimThatIsGeaterThan(ClaimsPrincipal p, string claim, double value)
    {
        if (p == null || p.Claims == null || p.Claims.Any() == false)
            return false;

        return p.Claims.Any(c => string.Equals(c.Type, claim, StringComparison.OrdinalIgnoreCase)
            && c.Value != null && double.TryParse(c.Value, out var parsed) && parsed > value);
    }

    public static bool AnyClaimThatIsGeaterThanOrEqual(ClaimsPrincipal p, string claim, double value)
    {
        if (p == null || p.Claims == null || p.Claims.Any() == false)
            return false;

        return p.Claims.Any(c => string.Equals(c.Type, claim, StringComparison.OrdinalIgnoreCase)
            && c.Value != null && double.TryParse(c.Value, out var parsed) && parsed >= value);
    }

    public static bool AnyClaimThatIsLessThan(ClaimsPrincipal p, string claim, double value)
    {
        if (p == null || p.Claims == null || p.Claims.Any() == false)
            return false;

        return p.Claims.Any(c => string.Equals(c.Type, claim, StringComparison.OrdinalIgnoreCase)
            && c.Value != null && double.TryParse(c.Value, out var parsed) && parsed < value);
    }

    public static bool AnyClaimThatIsLessThanOrEqual(ClaimsPrincipal p, string claim, double value)
    {
        if (p == null || p.Claims == null || p.Claims.Any() == false)
            return false;

        return p.Claims.Any(c => string.Equals(c.Type, claim, StringComparison.OrdinalIgnoreCase)
            && c.Value != null && double.TryParse(c.Value, out var parsed) && parsed <= value);
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