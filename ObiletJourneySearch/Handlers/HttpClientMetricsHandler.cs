using Prometheus;
using System.Diagnostics;

namespace ObiletJourneySearch.Handlers
{
    public class HttpClientMetricsHandler : DelegatingHandler
    {
        // Prometheus histogram metrik oluşturma
        private static readonly Histogram RequestDuration = Metrics.CreateHistogram("httpclient_request_duration_seconds", "HTTP Client request duration in seconds",
                new HistogramConfiguration
                {
                    LabelNames = new[] { "method", "endpoint", "status_code" }
                });

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var stopwatch = Stopwatch.StartNew();

            var response = await base.SendAsync(request, cancellationToken);

            stopwatch.Stop();

            var path = request.RequestUri?.AbsolutePath ?? "";

            RequestDuration.Labels(request.Method.Method, path, ((int)response.StatusCode).ToString()).Observe(stopwatch.Elapsed.TotalSeconds);

            return response;
        }
    }
}
