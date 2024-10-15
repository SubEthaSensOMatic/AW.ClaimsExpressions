# AW.ClaimsExpressions

This project provides a simple Domain-Specific Language (DSL) for validating JWT claims. It defines an expressive grammar that allows users to create complex claim validation rules using logical operators (and, or, not) and claim checks (e.g., exists, =, <, <=, >, >=, contains, startsWith, endsWith).

## Features

* Supports logical operators (and, or, not) for combining multiple validation rules.
* Checks for claims using operators like =, <, >, <=, >=, contains, startsWith, and endsWith.
* Compiles validation rules into C# functions.

## Grammar

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

## ASP.NET Core integration

### Configure claims expressions

To secure your endpoints, define your claims validation expressions in your configuration (e.g., `appsettings.json`):

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

In your ASP.NET Core project, register the services provided by this library in `Startup.cs` (or `Program.cs`):

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

Use the `AuthorizeByClaimsExpression` attribute to apply the claims validation policy to your controller actions:

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
