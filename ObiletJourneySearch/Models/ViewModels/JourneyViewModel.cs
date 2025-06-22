using ObiletJourneySearch.Models.Api.ResponseModels;

namespace ObiletJourneySearch.Models.ViewModels
{
    public class JourneyViewModel
    {
        public List<JourneyResponse.JourneyItem> Journeys { get; set; }
        public int OriginId { get; set; }
        public int DestinationId { get; set; }
        public DateTime DepartureDate { get; set; }
        public string OriginName { get; set; }
        public string DestinationName { get; set; }
    }
}
