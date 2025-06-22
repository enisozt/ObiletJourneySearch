using ObiletJourneySearch.Models.Api.RequestModels;
using ObiletJourneySearch.Models.Api.ResponseModels;

namespace ObiletJourneySearch.Services.Abstract
{
    public interface IObiletService
    {
        Task<List<BusLocationResponse.DataItem>> GetBusLocationsAsync(string sessionId, string deviceId, string search = "");
        Task<GetSessionResponse.SessionData> CreateSessionAsync();
        Task<List<JourneyResponse.JourneyItem>> GetJourneysAsync(JourneyRequest request);
    }
}
