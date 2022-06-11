using Xunit;
using Amazon.Lambda.TestUtilities;

namespace DemoLambda.Tests;

public class FunctionTest
{
    [Fact]
    public async Task TestToUpperFunction()
    {
        var function = new Function();
        var context = new TestLambdaContext();

        string result = await function.FunctionHandler("input", context);


        Assert.Equal("INPUT", result);
    }
}
