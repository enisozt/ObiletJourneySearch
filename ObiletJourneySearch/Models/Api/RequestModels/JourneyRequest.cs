using System.Text.Json.Serialization;

namespace ObiletJourneySearch.Models.Api.RequestModels
{
    public class JourneyRequest
    {
        [JsonPropertyName("data")]
        public DataModel Data { get; set; }

        [JsonPropertyName("device-session")]
        public DeviceSessions DeviceSession { get; set; }

        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        public class DataModel
        {
            [JsonPropertyName("origin-id")]
            public int OriginId { get; set; }

            [JsonPropertyName("destination-id")]
            public int DestinationId { get; set; }

            [JsonPropertyName("departure-date")]
            public string DepartureDate { get; set; }
        }

        public class DeviceSessions
        {
            [JsonPropertyName("session-id")]
            public string SessionId { get; set; }

            [JsonPropertyName("device-id")]
            public string DeviceId { get; set; }
        }
    }
}
