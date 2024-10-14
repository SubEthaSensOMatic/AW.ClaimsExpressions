using System.Security.Claims;

namespace AW.ClaimsExpressions;

public delegate bool ClaimsExpressionValidator(ClaimsPrincipal principal);
