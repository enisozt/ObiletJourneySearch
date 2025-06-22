using Microsoft.Extensions.Options;
using ObiletJourneySearch.ApiClients.Abstract;
using ObiletJourneySearch.ApiClients.Concrete;
using ObiletJourneySearch.Handlers;
using ObiletJourneySearch.Models.Api;
using ObiletJourneySearch.Policies;
using ObiletJourneySearch.Services.Abstract;
using ObiletJourneySearch.Services.Concrete;
using System.Net.Http.Headers;

namespace ObiletJourneySearch.Extensions
{
    public static class HttpClientConfigExtensions
    {
        public static IServiceCollection AddExternalClients(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<ObiletApiSettings>(config.GetSection("ObiletApiSettings"));

            services.AddHttpClient<IObiletApiClient, ObiletApiClient>((serviceProvider, client) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<ObiletApiSettings>>().Value;

                client.BaseAddress = new Uri(options.BaseUrl);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", options.ClientToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            })
            .AddHttpMessageHandler<HttpClientMetricsHandler>()
            .AddHttpMessageHandler<ErrorLogHandler>()
            .AddPolicyHandler(PollyPolicy.GetRetryPolicy())
            .AddPolicyHandler(PollyPolicy.GetCircuitBreakerPolicy());

            services.AddHttpClient<IObiletApiClient, ObiletApiClient>();

            services.AddScoped<IObiletService, ObiletService>();
            services.AddTransient<ErrorLogHandler>();
            services.AddTransient<HttpClientMetricsHandler>();
            services.AddScoped<ICacheService, MemoryCacheService>();

            services.AddMemoryCache();

            return services;
        }
    }
}
