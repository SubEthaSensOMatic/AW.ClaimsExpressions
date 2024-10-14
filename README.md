# AW.ClaimsExpressions

This project provides a simple Domain-Specific Language (DSL) for validating JWT claims. It defines an expressive grammar that allows users to create complex claim validation rules using logical operators (and, or, not) and claim checks (e.g., exists, equals, contains, startsWith, endsWith).

## Features

* Supports logical operators (and, or, not) for combining multiple validation rules.
* Checks for claims using operators like equals, contains, startsWith, and endsWith.
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
Primary         -> 'exists' Claim 
                 | Claim 'equals' String 
                 | Claim 'contains' String 
                 | Claim 'startsWith' String 
                 | Claim 'endsWith' String 
                 | '(' Expression ')'
```

## Example expressions

* `exists [is_admin]` — checks if the `is_admin` claim exists.
* `[role] equals 'admin'` — checks if the `role` claim equals `admin`.
* `[scope] contains 'read'` — checks if the `scope` claim contains the substring `read`.
* `not [verified] equals 'true'` — checks if the `verified` claim is not equal to `true`.
* `([role] equals 'admin') and ([email] contains 'example.com')` — combines multiple conditions with `and`.
