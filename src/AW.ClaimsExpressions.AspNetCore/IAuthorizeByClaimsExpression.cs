using System.Security.Claims;
using System.Threading.Tasks;

namespace AW.ClaimsExpressions.AspNetCore;

/// <summary>
/// Check if user is authorized
/// </summary>
public interface IAuthorizeByClaimsExpression
{
    /// <summary>
    /// Check if user is authorized
    /// </summary>
    /// <param name="user">User</param>
    /// <param name="configurationKey">Configuration key which contains claims expression</param>
    /// <returns></returns>
    Task<bool> IsAuthorized(ClaimsPrincipal user, string configurationKey);
}
