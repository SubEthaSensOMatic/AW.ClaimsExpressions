using System.Security.Claims;
using System.Threading.Tasks;

namespace AW.ClaimsExpressions.Test;

[TestClass]
public class ValidatorTests
{
    private static ClaimsPrincipal CreatePrincipal(params Claim[] claims)
        => new(new ClaimsIdentity(claims, "Bearer"));

    [TestMethod]
    public async Task Test_ClaimExists()
    {
        var principal = CreatePrincipal(new Claim("role", "admin"));

        var validator = await Compiler.Compile("exists [role]");

        Assert.IsTrue(validator(principal));
    }

    [TestMethod]
    public async Task Test_ClaimEqualsString()
    {
        var principal = CreatePrincipal(new Claim("role", "admin"));

        var validator = await Compiler.Compile("[role] equals 'admin'");

        Assert.IsTrue(validator(principal));
    }

    [TestMethod]
    public async Task Test_ClaimContainsSubstring()
    {
        var principal = CreatePrincipal(new Claim("email", "user@example.com"));

        var validator = await Compiler.Compile("[email] contains 'example'");

        Assert.IsTrue(validator(principal));
    }

    [TestMethod]
    public async Task Test_ClaimStartsWith()
    {
        var principal = CreatePrincipal(new Claim("email", "user@example.com"));

        var validator = await Compiler.Compile("[email] startsWith 'user'");

        Assert.IsTrue(validator(principal));
    }

    [TestMethod]
    public async Task Test_ClaimEndsWith()
    {
        var principal = CreatePrincipal(new Claim("email", "user@example.com"));

        var validator = await Compiler.Compile("[email] endsWith 'com'");

        Assert.IsTrue(validator(principal));
    }

    [TestMethod]
    public async Task Test_ClaimDoesNotExist()
    {
        var principal = CreatePrincipal(new Claim("role", "admin"));

        var validator = await Compiler.Compile("not exists [email]");

        Assert.IsTrue(validator(principal));
    }

    [TestMethod]
    public async Task Test_ComplexExpression_OrAnd()
    {
        var principal = CreatePrincipal(
            new Claim("role", "admin"),
            new Claim("email", "user@example.com")
        );

        var validator = await Compiler.Compile("[role] equals 'admin' and [email] contains 'example'");

        Assert.IsTrue(validator(principal));
    }

    [TestMethod]
    public async Task Test_ComplexExpression_NotAnd()
    {
        var principal = CreatePrincipal(
            new Claim("role", "user"),
            new Claim("email", "user@example.com")
        );

        var validator = await Compiler.Compile("not [role] equals 'admin' and [email] startsWith 'user'");

        Assert.IsTrue(validator(principal));
    }
}
