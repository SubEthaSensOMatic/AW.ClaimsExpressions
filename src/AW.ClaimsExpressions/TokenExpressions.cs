using System.Text.RegularExpressions;

namespace AW.ClaimsExpressions;

public partial class TokenExpressions
{
    [GeneratedRegex(@"^\u005b([^\u005b\u005d\u005C]|\u005C\u005b|\u005C\u005d|\u005C\u005C)+\u005d", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex CLAIMS_EXPRESSION();

    [GeneratedRegex(@"^'([^'\u005C]|\u005C'|\u005C\u005C)*'", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex STRING_EXPRESSION();

    [GeneratedRegex(@"^\s+", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex WHITESPACE_EXPRESSION();

    [GeneratedRegex(@"^\)", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex RIGHT_PARENTHESIS_EXPRESSION();

    [GeneratedRegex(@"^\(", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex LEFT_PARENTHESIS_EXPRESSION();
    
    [GeneratedRegex(@"^exists", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex EXISTS_EXPRESSION();
    
    [GeneratedRegex(@"^contains", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex CONTAINS_EXPRESSION();

    [GeneratedRegex(@"^startsWith", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex STARTS_WITH_EXPRESSION();

    [GeneratedRegex(@"^endsWith", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex ENDS_WITH_EXPRESSION();

    [GeneratedRegex(@"^equals", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex EQUALS_EXPRESSION();
    
    [GeneratedRegex(@"^or", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex OR_EXPRESSION();
    
    [GeneratedRegex(@"^and", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex AND_EXPRESSION();

    [GeneratedRegex(@"^not", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex NOT_EXPRESSION();

    /// <summary>
    /// Alle Tokens (in Reihenfolge, die der Tokenizer benötigt)
    /// </summary>
    public static (Regex, TokenTypes)[] ALL_TOKENS =
    {
        (STRING_EXPRESSION(), TokenTypes.String),
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
        (EXISTS_EXPRESSION(), TokenTypes.Exists)
    };
}
