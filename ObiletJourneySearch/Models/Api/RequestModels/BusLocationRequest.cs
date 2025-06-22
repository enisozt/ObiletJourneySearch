using System.Text.Json.Serialization;

namespace ObiletJourneySearch.Models.Api.RequestModels
{
    public class BusLocationRequest
    {
        [JsonPropertyName("data")]
        public object Data { get; set; } = null;

        [JsonPropertyName("device-session")]
        public DeviceSessions DeviceSession { get; set; }

        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; } = "tr-TR";

        public class DeviceSessions
        {
            [JsonPropertyName("session-id")]
            public string SessionId { get; set; }

            [JsonPropertyName("device-id")]
            public string DeviceId { get; set; }
        }
    }
}
