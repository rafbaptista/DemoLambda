namespace DemoLambda.Application.Interfaces
{
    public interface IFunctionService
    {
        Task<string> RunAsync(string input);
    }
}
