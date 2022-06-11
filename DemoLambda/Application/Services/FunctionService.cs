using DemoLambda.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace DemoLambda.Application.Services
{
    public class FunctionService : IFunctionService
    {
        private readonly ILogger<FunctionService> _logger;

        public FunctionService(ILogger<FunctionService> logger)
        {
            _logger = logger;
        }

        public async Task<string> RunAsync(string input)
        {
            _logger.LogInformation("FunctionService:RunAsync executando tarefa...");
            return input.ToUpper();
        }
    }
}
