using Microsoft.Extensions.Caching.Memory;
using ObiletJourneySearch.ApiClients.Abstract;
using ObiletJourneySearch.Models.Api.RequestModels;
using ObiletJourneySearch.Models.Api.ResponseModels;
using ObiletJourneySearch.Services.Abstract;

namespace ObiletJourneySearch.Services.Concrete
{
    public class ObiletService : IObiletService
    {
        private readonly IObiletApiClient _apiClient;
        private readonly IMemoryCache _cache;

        public ObiletService(IObiletApiClient apiClient, IMemoryCache cache)
        {
            _apiClient = apiClient;
            _cache = cache;
        }

        public async Task<List<BusLocationResponse.DataItem>> GetBusLocationsAsync(string sessionId, string deviceId, string search = null)
        {
            var cacheKey = $"bus_locations_all";
            if (_cache.TryGetValue(cacheKey, out List<BusLocationResponse.DataItem> cachedLocations))
            {
                return cachedLocations;
            }
            var locations = await _apiClient.GetBusLocationsAsync(sessionId, deviceId, search);

            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
            };

            _cache.Set(cacheKey, locations, cacheOptions);

            return locations;
        }

        public async Task<GetSessionResponse.SessionData> CreateSessionAsync()
        {
            var sessionResponse = await _apiClient.GetSessionAsync();
            return sessionResponse.Data;
        }

        public async Task<List<JourneyResponse.JourneyItem>> GetJourneysAsync(JourneyRequest request)
        {
            return await _apiClient.GetJourneysAsync(request);
        }
    }
}
