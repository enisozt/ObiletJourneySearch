using System.Text.Json.Serialization;

namespace ObiletJourneySearch.Models.Api.ResponseModels
{
    public class JourneyResponse
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("data")]
        public List<JourneyItem> Data { get; set; }

        public class JourneyItem
        {
            [JsonPropertyName("partner-name")]
            public string PartnerName { get; set; }

            [JsonPropertyName("bus-type-name")]
            public string BusType { get; set; }

            [JsonPropertyName("journey")]
            public JourneyDetail Journey { get; set; }

            public class JourneyDetail
            {
                [JsonPropertyName("departure")]
                public string Departure { get; set; }

                [JsonPropertyName("arrival")]
                public string Arrival { get; set; }

                [JsonPropertyName("duration")]
                public string Duration { get; set; }

                [JsonPropertyName("internet-price")]
                public decimal Price { get; set; }

                [JsonPropertyName("currency")]
                public string Currency { get; set; }

                [JsonPropertyName("features")]
                public List<string> Features { get; set; }
            }
        }
    }
}
