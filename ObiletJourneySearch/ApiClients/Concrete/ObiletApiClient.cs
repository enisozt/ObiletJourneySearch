using Microsoft.Extensions.Options;
using ObiletJourneySearch.ApiClients.Abstract;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using ObiletJourneySearch.Models.Api.ResponseModels;
using ObiletJourneySearch.Models.Api.RequestModels;
using ObiletJourneySearch.Models.Api;

namespace ObiletJourneySearch.ApiClients.Concrete
{
    public class ObiletApiClient : IObiletApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ObiletApiSettings _settings;
        private readonly JsonSerializerOptions _serializerOptions;

        public ObiletApiClient(HttpClient httpClient, IOptions<ObiletApiSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;

            _httpClient.BaseAddress = new Uri(_settings.BaseUrl);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _settings.ClientToken);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };
        }
        public async Task<GetSessionResponse> GetSessionAsync()
        {
            var json = @"{
                ""type"":1,
                ""connection"":{
                    ""ip-address"":""165.114.41.21"",
                    ""port"":""5117""
                },
                ""browser"":{
                    ""name"":""Chrome"",
                    ""version"":""47.0.0.12""
                }
            }";

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/client/getsession", content);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Session alma başarısız: {response.StatusCode}");

            var resultJson = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GetSessionResponse>(resultJson, _serializerOptions);
        }


        public async Task<List<BusLocationResponse.DataItem>> GetBusLocationsAsync(string sessionId, string deviceId, string searchText = null)
        {
            var request = new BusLocationRequest
            {
                Data = null,
                DeviceSession = new BusLocationRequest.DeviceSessions
                {
                    SessionId = sessionId,
                    DeviceId = deviceId
                },
                Date = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss"),
                Language = "tr-TR"
            };

            var jsonBody = JsonSerializer.Serialize(request);

            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/location/getbuslocations", content);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Lokasyonlar çekilemedi: {response.StatusCode}");

            var resultJson = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<BusLocationResponse>(resultJson, _serializerOptions);

            return result?.Data ?? new();
        }



        public async Task<List<JourneyResponse.JourneyItem>> GetJourneysAsync(JourneyRequest request)
        {
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/journey/getbusjourneys", content);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Seferler alınamadı");

            using var stream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<JourneyResponse>(stream, _serializerOptions);

            return result?.Data ?? new();
        }
    }
}
