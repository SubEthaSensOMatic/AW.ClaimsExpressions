namespace AW.ClaimsExpressions.Test;

[TestClass]
public class TokenExpressionTests
{
    [DataTestMethod]
    [DataRow("[a]", "[a]")]
    [DataRow("[abc] [", "[abc]")]
    [DataRow("[a-b_c] ]", "[a-b_c]")]
    [DataRow(@"[a\[b\]]    ...", @"[a\[b\]]")]
    [DataRow(@"[ab\\c]", @"[ab\\c]")]
    public void TestValidClaims(string expression, string expectedValue)
        => Assert.AreEqual(TokenExpressions.CLAIMS_EXPRESSION().Match(expression).Value, expectedValue);

    [DataTestMethod]
    [DataRow("[a")]
    [DataRow("c]")]
    [DataRow("[]")]
    [DataRow(@"[a[b]]")]
    [DataRow(@"[ab\c]")]
    public void TestInvalidClaims(string expression)
        => Assert.IsFalse(TokenExpressions.CLAIMS_EXPRESSION().IsMatch(expression));

    [DataTestMethod]
    [DataRow("'' 'asdasd'", "''")]
    [DataRow("'a'", "'a'")]
    [DataRow("'abc' []", "'abc'")]
    [DataRow("'a\\'c' '123'", "'a\\'c'")]
    [DataRow("'ab\\\\c'", "'ab\\\\c'")]
    public void TestValidStrings(string expression, string expectedValue)
        => Assert.AreEqual(TokenExpressions.STRING_EXPRESSION().Match(expression).Value, expectedValue);

    [DataTestMethod]
    [DataRow("'")]
    [DataRow("a'")]
    [DataRow("'a")]
    [DataRow("'ab\\\\\\c'")]
    public void TestInvalidStrings(string expression)
        => Assert.IsFalse(TokenExpressions.STRING_EXPRESSION().IsMatch(expression));
}