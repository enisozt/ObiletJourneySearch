using System.Text.Json.Serialization;

namespace ObiletJourneySearch.Models.Api.RequestModels
{
    public class GetSessionRequest
    {
        [JsonPropertyName("type")]
        public int Type { get; set; }

        [JsonPropertyName("connection")]
        public Connections Connection { get; set; }

        [JsonPropertyName("browser")]
        public Browsers Browser { get; set; }
        public Applications Application { get; set; }

        public class Connections
        {
            [JsonPropertyName("ip-address")]
            public string IpAddress { get; set; }

            [JsonPropertyName("port")]
            public string Port { get; set; }
        }

        public class Browsers
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("version")]
            public string Version { get; set; }
        }
        public class Applications
        {
            public string version { get; set; }
            public string equipmentid { get; set; }
        }
    }
}
