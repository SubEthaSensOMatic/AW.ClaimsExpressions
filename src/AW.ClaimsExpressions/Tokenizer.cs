using System;
using System.Collections.Generic;

namespace AW.ClaimsExpressions;

public record Token(string Value, TokenTypes Type, int Start, int Length);

public static class Tokenizer
{
    public static List<Token> Tokenize(string expression)
    {
        var tokens = new List<Token>();
        var position = 0;

        while (position < expression.Length)
        {
            var matched = false;

            // Ohne Substring und ^ in der Expression funktioniert leider Match nicht.
            var part = expression[position..];

            foreach (var (tokenExpression, tokenType) in TokenExpressions.ALL_TOKENS)
            {
                var match = tokenExpression.Match(part);
                
                if (match != null && match.Success)
                {
                    tokens.Add(new Token(match.Value, tokenType, position, match.Length));
                    position += match.Length;
                    matched = true;
                    break;
                }
            }

            if (matched == false)
            {
                throw new Exception($"Syntax error at position {position}.");
            }
        }

        return tokens;
    }
}
