using Amazon.S3;
using Amazon.S3.Model;
using DemoLambda.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace DemoLambda.Application.Services
{
    public class FunctionService : IFunctionService
    {
        private readonly ILogger<FunctionService> _logger;
        private readonly IAmazonS3 _s3Client;

        public FunctionService(ILogger<FunctionService> logger, IAmazonS3 s3Client)
        {
            _logger = logger;
            _s3Client = s3Client;
        }

        public async Task<string> RunAsync(string input)
        {
            _logger.LogInformation("FunctionService:RunAsync executando tarefa...");

            GetObjectResponse s3Response = await _s3Client.GetObjectAsync(new GetObjectRequest
            {
                BucketName = "demo-bucket-rafa",
                Key = "file.txt",                
            });

            string fileContent = string.Empty;
            using (var sr = new StreamReader(s3Response.ResponseStream))
            {
                fileContent = sr.ReadToEnd();
            }

            return fileContent;
        }
    }
}
