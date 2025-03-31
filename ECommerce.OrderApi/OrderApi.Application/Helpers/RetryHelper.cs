using Polly;
using Polly.Registry;

namespace OrderApi.Application.Helpers;

public class RetryHelper(ResiliencePipelineProvider<string> pipelineProvider)
{
    public const string RETRY_PIPELINE= "order-retry-pipeline";

    private readonly ResiliencePipeline _resiliencePipeline = pipelineProvider.GetPipeline(RETRY_PIPELINE);

    public async Task<T> ExecuteAsync<T>(Func<Task<T>> action, CancellationToken token)
    {
        return await _resiliencePipeline.ExecuteAsync(async _token => await action(), token);
    }
}
