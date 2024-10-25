# AW.ClaimsExpressions

**AW.ClaimsExpressions** is a library that offers a Domain-Specific Language (DSL) for dynamically validating JWT claims. It allows for the creation of expressive and flexible validation rules using a concise grammar. With this library, developers can define complex claim validation logic through logical operators and various claim checks, all without needing to modify code.

## Features

* **Configurable validation rules:** Easily configure and update claim validation policies, without the need for code changes, making the solution adaptable to evolving requirements.
* **Logical operators:** Supports `and`, `or`, and `not` for combining multiple conditions, enabling the creation of sophisticated rules.
* **Flexible claim checks:** Perform checks for claims using operators like `=`, `<`, `>` , `<=`, `>=`, `contains`, `startsWith`, and `endsWith`.
* **Compilation to C# Functions:** Converts DSL expressions into efficient C# functions, making claim validation both powerful and fast.

## Installation

You can add this library to your project via NuGet

- [AW.ClaimsExpressions](https://www.nuget.org/packages/AW.ClaimsExpressions/)

- [AW.ClaimsExpressions.AspNetCore](https://www.nuget.org/packages/AW.ClaimsExpressions.AspNetCore/)


## Grammar overview

The DSL is built around the following grammar:

```
Expression      -> OrExpression
OrExpression    -> AndExpression ('or' AndExpression)*
AndExpression   -> NotExpression ('and' NotExpression)*
NotExpression   -> 'not' NotExpression | Primary
Claim           -> '[' text ']'
String          -> '\'' text '\''
Integer         -> [+-]?[0-9]+
Float           -> [+-]?([0-9]*[.])?[0-9]+
Primary         -> 'exists' Claim 
                 | Claim '=' String | Integer | Float
                 | Claim '>' String | Integer | Float
                 | Claim '<' String | Integer | Float
                 | Claim '>=' String | Integer | Float
                 | Claim '<=' String | Integer | Float
                 | Claim 'contains' String 
                 | Claim 'startsWith' String 
                 | Claim 'endsWith' String 
                 | '(' Expression ')'
```

## Example expressions

* `exists [is_admin]` — checks if the `is_admin` claim exists.
* `[role] = 'admin'` — checks if the `role` claim equals `admin`.
* `[scope] contains 'read'` — checks if the `scope` claim contains the substring `read`.
* `not [verified] = 'true'` — checks if the `verified` claim is not equal to `true`.
* `([role] = 'admin') and ([email] contains 'example.com')` — combines multiple conditions with `and`.

## Using the compiler

The main entry point is the Compiler `AW.ClaimsExpressions.Compiler`, which compiles an expression string into the validator delegate.

```
var expression = "[email] endsWith '@example.com' and [role] = 'admin'";
var validator = await Compiler.Compile(expression);

ClaimsPrincipal user = /* obtain the ClaimsPrincipal */;
bool isValid = validator(user);
```

## Seamless ASP.NET Core integration

### Configure claims expressions

**AW.ClaimsExpressions** has the ability to support dynamic, configurable claim validation. Validation rules can be defined in external configuration files (like appsettings.json) or environment variables, allowing for easy updates without altering the underlying codebase.

For example, define policies in your configuration file:
```
{
  ...

  "ClaimsPolicies": {
    "AdminPolicy": "[role] = 'admin'",
    "RegionPolicy": "[region] = 'US'",
    "AdultPolicy": "[age] >= 18",
    "AdvancedPolicy": "[role] = 'admin' and [region] = 'US'"
  },

  ...
}
```

### Register services

Integrating AW.ClaimsExpressions into an ASP.NET Core project is straightforward. Register the services provided by this library and configure JWT authentication in `Startup.cs` (or `Program.cs`):

```
public void ConfigureServices(IServiceCollection services)
{
    // Register JWT claim expression services
    services.AddClaimsExpressions();

    services.AddControllers();

    // Configure JWT authorization
    services.AddAuthentication("Bearer")
            .AddJwtBearer(options => { /* JWT settings */ });
}
```

### Applying authorization to endpoints

Apply the validation policies directly to your controllers with the `[AuthorizeByClaimsExpression]` attribute:

```
[AuthorizeByClaimsExpression("ClaimsPolicies:AdminPolicy")]
[HttpGet("admin-only")]
public IActionResult AdminOnly()
{
    return Ok("Welcome, admin!");
}

[AuthorizeByClaimsExpression("ClaimsPolicies:RegionPolicy")]
[HttpGet("only-us")]
public IActionResult OnlyUsRegion()
{
    return Ok("Welcome from the US!");
}


[AuthorizeByClaimsExpression("ClaimsPolicies:AdultPolicy")]
[HttpGet("adult-only")]
public IActionResult AdultOnly()
{
    return Ok("Welcome, adult!");
}
```

### Applying authorization to services

Use the `IAuthorizeByClaimsExpression` service to apply the claims validation policy to your services:

```
public class SomeService
{
    private readonly IAuthorizeByClaimsExpression _authorize;

    public SomeService(IAuthorizeByClaimsExpression authorize)
        => _authorize = authorize;

    public Task DoSomething()
    {
        ClaimsPrincipal user = /* obtain the ClaimsPrincipal */;

        // Check user is admin
        if (_authorize.IsAuthorized(user, "ClaimsPolicies:AdminPolicy") == false)
            throw Exception("Not authorized!");

        ...
    }
}
```

## License
This project is licensed under the MIT License.