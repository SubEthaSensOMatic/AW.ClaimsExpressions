using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AW.ClaimsExpressions;

public static class Compiler
{
    private static readonly SemaphoreSlim _lock = new(1, 1);
    private static readonly Dictionary<string, ClaimsExpressionValidator> _cache = [];

    public static async Task<ClaimsExpressionValidator> Compile(string expression)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(expression, nameof(expression));

        await _lock.WaitAsync();

        try
        {
            if (_cache.TryGetValue(expression, out var validator))
                return validator;

            // Tokens extrahieren... (ohne Whitespaces)
            var tokens = Tokenizer
                .Tokenize(expression)
                .Where(t => t.Type != TokenTypes.Whitespace)
                .ToArray();

            // AST bauen...
            var tree = new Parser(tokens).Parse();

            // AST in C# umwandeln
            var code = new StringBuilder();
            code.Append("p => (");
            BuildDotNetExpression(tree, code);
            code.Append(')');

            // Kompilieren
            var options = ScriptOptions.Default
                .AddReferences(typeof(ClaimsPrincipalExtensions).Assembly, typeof(ClaimsPrincipal).Assembly);

            validator = await CSharpScript.EvaluateAsync<ClaimsExpressionValidator>(code.ToString(), options);

            return _cache[expression] = validator;
        }
        finally
        {
            _lock.Release();
        }
    }

    private static string GetClaimFromToken(Token claimToken)
    {
        if (claimToken.Type != TokenTypes.Claim)
            throw new InvalidOperationException($"Token has to be of type '{TokenTypes.Claim}'.");

        return claimToken.Value[1..^1].Replace(@"\\", @"\").Replace(@"\[", "[").Replace(@"\]", "]");
    }

    private static string GetStringFromToken(Token stringToken)
    {
        if (stringToken.Type != TokenTypes.String)
            throw new InvalidOperationException($"Token has to be of type '{TokenTypes.String}'.");

        return stringToken.Value[1..^1].Replace(@"\\", @"\").Replace(@"\'", "'");
    }

    private static void BuildDotNetExpression(Node node, StringBuilder code)
    {
        if (node is ExistsNode existsNode)
        {
            var claim = GetClaimFromToken(existsNode.Claim);
            code.Append($"AW.ClaimsExpressions.ClaimsPrincipalExtensions.ExistsClaim(p, \"{claim}\")");
        }
        else if (node is ContainsNode likeNode)
        {
            var claim = GetClaimFromToken(likeNode.Claim);
            var value = GetStringFromToken(likeNode.Value);
            code.Append($"AW.ClaimsExpressions.ClaimsPrincipalExtensions.AnyClaimThatContains(p, \"{claim}\",\"{value}\")");
        }
        else if (node is EqualsNode equalsNode)
        {
            var claim = GetClaimFromToken(equalsNode.Claim);
            var value = GetStringFromToken(equalsNode.Value);
            code.Append($"AW.ClaimsExpressions.ClaimsPrincipalExtensions.AnyClaimThatEquals(p, \"{claim}\",\"{value}\")");
        }
        else if (node is StartsWithNode startsWithNode)
        {
            var claim = GetClaimFromToken(startsWithNode.Claim);
            var value = GetStringFromToken(startsWithNode.Value);
            code.Append($"AW.ClaimsExpressions.ClaimsPrincipalExtensions.AnyClaimThatStartsWith(p, \"{claim}\",\"{value}\")");
        }
        else if (node is EndsWithNode endsWithNode)
        {
            var claim = GetClaimFromToken(endsWithNode.Claim);
            var value = GetStringFromToken(endsWithNode.Value);
            code.Append($"AW.ClaimsExpressions.ClaimsPrincipalExtensions.AnyClaimThatEndsWith(p, \"{claim}\",\"{value}\")");
        }
        else if (node is NotNode notNode)
        {
            code.Append("(false == (");
            BuildDotNetExpression(notNode.Negated, code);
            code.Append("))");
        }
        else if (node is AndNode andNode)
        {
            code.Append('(');
            BuildDotNetExpression(andNode.Left, code);
            code.Append(") && (");
            BuildDotNetExpression(andNode.Right, code);
            code.Append(')');
        }
        else if (node is OrNode orNode)
        {
            code.Append('(');
            BuildDotNetExpression(orNode.Left, code);
            code.Append(") || (");
            BuildDotNetExpression(orNode.Right, code);
            code.Append(')');
        }
    }
}
