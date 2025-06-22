using System.Text.Json.Serialization;

namespace ObiletJourneySearch.Models.Api.ResponseModels
{
    public class GetSessionResponse
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("data")]
        public SessionData Data { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        public class SessionData
        {
            [JsonPropertyName("session-id")]
            public string SessionId { get; set; }

            [JsonPropertyName("device-id")]
            public string DeviceId { get; set; }

            [JsonPropertyName("affiliate")]
            public object Affiliate { get; set; }

            [JsonPropertyName("device-type")]
            public int DeviceType { get; set; }

            [JsonPropertyName("device")]
            public object Device { get; set; }

            [JsonPropertyName("ip-country")]
            public string IpCountry { get; set; }

            [JsonPropertyName("clean-session-id")]
            public long CleanSessionId { get; set; }

            [JsonPropertyName("clean-device-id")]
            public long CleanDeviceId { get; set; }

            [JsonPropertyName("ip-address")]
            public string IpAddress { get; set; }
        }
    }
}
