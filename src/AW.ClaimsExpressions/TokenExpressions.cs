using System.Text.RegularExpressions;

namespace AW.ClaimsExpressions;

public partial class TokenExpressions
{
    [GeneratedRegex(@"^(?<token>\u005b([^\u005b\u005d\u005C]|\u005C\u005b|\u005C\u005d|\u005C\u005C)+\u005d)", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex CLAIMS_EXPRESSION();

    [GeneratedRegex(@"^(?<token>[+-]?[0-9]+)(?:$|\s|\))", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex INTEGER_EXPRESSION();

    [GeneratedRegex(@"^(?<token>[+-]?(?:[0-9]*\.)?[0-9]+)(?:$|\s|\))", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex FLOAT_EXPRESSION();

    [GeneratedRegex(@"^(?<token>'(?:[^'\u005C]|\u005C'|\u005C\u005C)*')", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex STRING_EXPRESSION();

    [GeneratedRegex(@"^(?<token>\s+)", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex WHITESPACE_EXPRESSION();

    [GeneratedRegex(@"^(?<token>\))", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex RIGHT_PARENTHESIS_EXPRESSION();

    [GeneratedRegex(@"^(?<token>\()", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex LEFT_PARENTHESIS_EXPRESSION();
    
    [GeneratedRegex(@"^(?<token>exists)", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex EXISTS_EXPRESSION();
    
    [GeneratedRegex(@"^(?<token>contains)", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex CONTAINS_EXPRESSION();

    [GeneratedRegex(@"^(?<token>startsWith)", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex STARTS_WITH_EXPRESSION();

    [GeneratedRegex(@"^(?<token>endsWith)", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex ENDS_WITH_EXPRESSION();

    [GeneratedRegex(@"^(?<token>=)", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex EQUALS_EXPRESSION();

    [GeneratedRegex(@"^(?<token>>)", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex GREATER_THAN_EXPRESSION();

    [GeneratedRegex(@"^(?<token><)", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex LESS_THAN_EXPRESSION();

    [GeneratedRegex(@"^(?<token>>=)", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex GREATER_THAN_OR_EQUAL_EXPRESSION();

    [GeneratedRegex(@"^(?<token><=)", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex LESS_THAN_OR_EQUAL_EXPRESSION();

    [GeneratedRegex(@"^(?<token>or)", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex OR_EXPRESSION();
    
    [GeneratedRegex(@"^(?<token>and)", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex AND_EXPRESSION();

    [GeneratedRegex(@"^(?<token>not)", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex NOT_EXPRESSION();

    /// <summary>
    /// Alle Tokens (in Reihenfolge, die der Tokenizer benötigt)
    /// </summary>
    public static readonly (Regex, TokenTypes)[] ALL_TOKENS =
    {
        (STRING_EXPRESSION(), TokenTypes.String),
        (INTEGER_EXPRESSION(), TokenTypes.Integer), // Erst Int dann Float, da Int auch in Float enthalten.
        (FLOAT_EXPRESSION(), TokenTypes.Float),
        (CLAIMS_EXPRESSION(), TokenTypes.Claim),
        (WHITESPACE_EXPRESSION(), TokenTypes.Whitespace),
        (LEFT_PARENTHESIS_EXPRESSION(), TokenTypes.LeftParenthesis),
        (RIGHT_PARENTHESIS_EXPRESSION(), TokenTypes.RightParenthesis),
        (NOT_EXPRESSION(), TokenTypes.Not),
        (AND_EXPRESSION(), TokenTypes.And),
        (OR_EXPRESSION(), TokenTypes.Or),
        (CONTAINS_EXPRESSION(), TokenTypes.Contains),
        (STARTS_WITH_EXPRESSION(), TokenTypes.StartsWith),
        (ENDS_WITH_EXPRESSION(), TokenTypes.EndsWith),
        (EQUALS_EXPRESSION(), TokenTypes.Equals),
        (GREATER_THAN_OR_EQUAL_EXPRESSION(), TokenTypes.GreaterOrEqual),
        (LESS_THAN_OR_EQUAL_EXPRESSION(), TokenTypes.LessOrEqual),
        (GREATER_THAN_EXPRESSION(), TokenTypes.Greater),
        (LESS_THAN_EXPRESSION(), TokenTypes.Less),
        (EXISTS_EXPRESSION(), TokenTypes.Exists)
    };
}
