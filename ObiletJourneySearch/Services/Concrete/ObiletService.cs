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
        private readonly ICacheService _cacheService;

        public ObiletService(IObiletApiClient apiClient, ICacheService cacheService)
        {
            _apiClient = apiClient;
            _cacheService = cacheService;
        }

        public async Task<List<BusLocationResponse.DataItem>> GetBusLocationsAsync(string sessionId, string deviceId, string search = null)
        {
            var cacheKey = "bus_locations_all";
            if (_cacheService.TryGetValue(cacheKey, out List<BusLocationResponse.DataItem> cachedLocations))
            {
                return cachedLocations;
            }

            var locations = await _apiClient.GetBusLocationsAsync(sessionId, deviceId, search);

            _cacheService.Set(cacheKey, locations, TimeSpan.FromHours(1));

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
