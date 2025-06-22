using ObiletJourneySearch.Models.Api.ResponseModels;

namespace ObiletJourneySearch.Models.ViewModels
{
    public class IndexViewModel
    {
        public List<BusLocationResponse.DataItem> Locations { get; set; }
        public int? SelectedOriginId { get; set; }
        public int? SelectedDestinationId { get; set; }
        public DateTime SelectedDate { get; set; } = DateTime.Today.AddDays(1);
    }
}
