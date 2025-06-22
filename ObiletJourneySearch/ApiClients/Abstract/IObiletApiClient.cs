using ObiletJourneySearch.Models.Api.RequestModels;
using ObiletJourneySearch.Models.Api.ResponseModels;

namespace ObiletJourneySearch.ApiClients.Abstract
{
    public interface IObiletApiClient
    {
        Task<GetSessionResponse> GetSessionAsync();
        Task<List<BusLocationResponse.DataItem>> GetBusLocationsAsync(string sessionId, string deviceId, string searchText = "");
        Task<List<JourneyResponse.JourneyItem>> GetJourneysAsync(JourneyRequest request);
    }
}
