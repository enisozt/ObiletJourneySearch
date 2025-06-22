namespace ObiletJourneySearch.Handlers
{
    public class ErrorLogHandler : DelegatingHandler
    {
        private readonly ILogger<ErrorLogHandler> _logger;

        public ErrorLogHandler(ILogger<ErrorLogHandler> logger)
        {
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await base.SendAsync(request, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Hata! Sunucu {Method} isteğine ({Uri}) {StatusCode} yanıtını verdi. Response: {Content}", response.StatusCode, request.Method, request.RequestUri, content);
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Method} isteği ({Uri}) sırasında beklenmeyen bir hata oluştu.", request.Method, request.RequestUri);
                throw;
            }
        }
    }
}
