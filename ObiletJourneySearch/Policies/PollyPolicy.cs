using Polly.Extensions.Http;
using Polly;

namespace ObiletJourneySearch.Policies
{
    public static class PollyPolicy
    {
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy() => HttpPolicyExtensions.HandleTransientHttpError().WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))); // 3 kez dene 

        public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy() => HttpPolicyExtensions.HandleTransientHttpError().CircuitBreakerAsync(3, TimeSpan.FromSeconds(30));
        //3 hata sonrası devreyi kes ve 30sn boyunca istekleri durdur
    }
}