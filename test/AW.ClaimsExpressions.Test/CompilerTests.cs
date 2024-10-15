using System;
using System.Threading.Tasks;

namespace AW.ClaimsExpressions.Test;

[TestClass]
public class CompilerTests
{
    [TestMethod]
    public Task Test_ExistsClaim()
        => Compiler.Compile("exists [email]");

    [TestMethod]
    public Task Test_ClaimEqualsString()
        => Compiler.Compile("[email] = 'user@example.com'");

    [TestMethod]
    public Task Test_ClaimContainsString()
        => Compiler.Compile("[email] contains '@example'");

    [TestMethod]
    public Task Test_ClaimStartsWithString()
        => Compiler.Compile("[email] startsWith 'example@'");

    [TestMethod]
    public Task Test_ClaimEndsWithString()
        => Compiler.Compile("[email] endsWith 'ample.com'");

    [TestMethod]
    public Task Test_AndExpression()
        => Compiler.Compile("[email] = 'user@example.com' and [role] = 'admin'");

    [TestMethod]
    public Task Test_OrExpression()
        => Compiler.Compile("[email] = 'user@example.com' or [email] = 'admin@example.com'");

    [TestMethod]
    public Task Test_NotExpression()
        => Compiler.Compile("not [email] = 'user@example.com'");

    [TestMethod]
    public Task Test_NestedExpression()
        => Compiler.Compile("(not [email] = 'user@example.com' and [role] = 'admin')");

    [TestMethod]
    public Task Test_ComplexExpression()
        => Compiler.Compile("(not [email] contains 'example.com' or [role] = 'admin') and exists [email]");

    [TestMethod]
    public Task Test_MissingClosingBracketInClaim()
        => Assert.ThrowsExceptionAsync<Exception>(() => Compiler.Compile("[email = 'user@example.com'"));

    [TestMethod]
    public Task Test_MissingClosingParenthesis()
        => Assert.ThrowsExceptionAsync<Exception>(() => Compiler.Compile("(not [email] = 'user@example.com'"));

    [TestMethod]
    public Task Test_UnsupportedOperator()
        => Assert.ThrowsExceptionAsync<Exception>(() => Compiler.Compile("[email] != 'user@example.com'"));

    [TestMethod]
    public Task Test_MissingEqualsKeyword()
        => Assert.ThrowsExceptionAsync<Exception>(() => Compiler.Compile("[email] 'user@example.com'"));

    [TestMethod]
    public Task Test_AndWithoutRightOperand()
        => Assert.ThrowsExceptionAsync<Exception>(() => Compiler.Compile("[email] = 'user@example.com' and"));

    [TestMethod]
    public Task Test_OrWithoutLeftOperand()
        => Assert.ThrowsExceptionAsync<Exception>(() => Compiler.Compile("or [email] = 'user@example.com'"));

    [TestMethod]
    public Task Test_InvalidNotUsage()
        => Assert.ThrowsExceptionAsync<Exception>(() => Compiler.Compile("not"));

    [TestMethod]
    public Task Test_InvalidStringFormat()
        => Assert.ThrowsExceptionAsync<Exception>(() => Compiler.Compile("[email] = 'user@example.com"));

    [TestMethod]
    public Task Test_MissingClaimInExists()
        => Assert.ThrowsExceptionAsync<Exception>(() => Compiler.Compile("exists"));

    [TestMethod]
    public Task Test_InvalidToken()
        => Assert.ThrowsExceptionAsync<Exception>(() => Compiler.Compile("[email] = 'user@example.com' Xand [role] = 'admin'"));
}