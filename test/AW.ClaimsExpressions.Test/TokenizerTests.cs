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
            new("equals", TokenTypes.Equals, 0, 6),
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
            new("exists", TokenTypes.Exists, 0, 6)
        };

        var expression = "[roles]   'string' or and not equals contains startsWith endsWith () exists";

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