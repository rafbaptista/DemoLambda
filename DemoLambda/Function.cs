using Amazon.Lambda.Core;
using DemoLambda.Application.Interfaces;
using DemoLambda.Interfaces;
using Microsoft.Extensions.DependencyInjection;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DemoLambda;

public class Function : BaseFunction, IFunctionHandler<Task<string>, string>
{
    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task<string> FunctionHandler(string input, ILambdaContext context)
    {
        var functionService = host.Services.GetService<IFunctionService>();

        return await functionService.RunAsync(input);
    }
}
