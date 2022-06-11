using Amazon.Lambda.Core;

namespace DemoLambda.Interfaces
{
    public interface IFunctionHandler<TReturn, TInput>
    {
        public TReturn FunctionHandler(TInput input, ILambdaContext context);
    }
}
