namespace ObiletJourneySearch.Models.Api.ResponseModels
{
    public class BusLocationResponse
    {
        public Status Statues { get; set; }
        public List<DataItem> Data { get; set; }

        public class Status
        {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
        }

        public class DataItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Slug { get; set; }
            public string Type { get; set; }
            public int? ParentId { get; set; }
            public int? CountryId { get; set; }
            public string? FullName { get; set; }
            public string? Latitude { get; set; }
            public string? Longitude { get; set; }
            public int Zoom { get; set; }
        }
    }
}
