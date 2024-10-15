namespace AW.ClaimsExpressions.Test;

[TestClass]
public class TokenizerTests
{
    [TestMethod]
    public void TestTokens()
    {
        var expectedTokens = new Token[]
        {
            new("[roles]", TokenTypes.Claim, 0, 7),
            new("   ", TokenTypes.Whitespace, 0, 3),
            new("'string'", TokenTypes.String, 0, 8),
            new(" ", TokenTypes.Whitespace, 0, 1),
            new("or", TokenTypes.Or, 0, 2),
            new(" ", TokenTypes.Whitespace, 0, 1),
            new("and", TokenTypes.And, 0, 3),
            new(" ", TokenTypes.Whitespace, 0, 1),
            new("not", TokenTypes.Not, 0, 3),
            new(" ", TokenTypes.Whitespace, 0, 1),
            new("=", TokenTypes.Equals, 0, 1),
            new(" ", TokenTypes.Whitespace, 0, 1),
            new(">", TokenTypes.Greater, 0, 1),
            new(" ", TokenTypes.Whitespace, 0, 1),
            new(">=", TokenTypes.GreaterOrEqual, 0, 2),
            new(" ", TokenTypes.Whitespace, 0, 1),
            new("<", TokenTypes.Less, 0, 1),
            new(" ", TokenTypes.Whitespace, 0, 1),
            new("<=", TokenTypes.LessOrEqual, 0, 2),
            new(" ", TokenTypes.Whitespace, 0, 1),
            new("contains", TokenTypes.Contains, 0, 8),
            new(" ", TokenTypes.Whitespace, 0, 1),
            new("startsWith", TokenTypes.StartsWith, 0, 10),
            new(" ", TokenTypes.Whitespace, 0, 1),
            new("endsWith", TokenTypes.EndsWith, 0, 8),
            new(" ", TokenTypes.Whitespace, 0, 1),
            new("(", TokenTypes.LeftParenthesis, 0, 1),
            new(")", TokenTypes.RightParenthesis, 0, 1),
            new(" ", TokenTypes.Whitespace, 0, 1),
            new("exists", TokenTypes.Exists, 0, 6),
            new(" ", TokenTypes.Whitespace, 0, 1),
            new("-0.123", TokenTypes.Float, 0, 6),
            new(" ", TokenTypes.Whitespace, 0, 1),
            new("+1000", TokenTypes.Integer, 0, 5),
            new(" ", TokenTypes.Whitespace, 0, 1),
            new("2000", TokenTypes.Integer, 0, 4),
            new(" ", TokenTypes.Whitespace, 0, 1),
            new("1.456", TokenTypes.Float, 0, 5)
        };

        var expression = "[roles]   'string' or and not = > >= < <= contains startsWith endsWith () exists -0.123 +1000 2000 1.456";

        var resultTokens = Tokenizer.Tokenize(expression);

        Assert.AreEqual(resultTokens.Count, expectedTokens.Length);
        
        for (var i = 0; i < expectedTokens.Length; i++)
        {
            Assert.AreEqual(expectedTokens[i].Value, resultTokens[i].Value);
            Assert.AreEqual(expectedTokens[i].Type, resultTokens[i].Type);
            Assert.AreEqual(expectedTokens[i].Length, resultTokens[i].Length);
        }
    }
}