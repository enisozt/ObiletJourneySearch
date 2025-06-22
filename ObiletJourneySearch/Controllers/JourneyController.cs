using Microsoft.AspNetCore.Mvc;
using ObiletJourneySearch.Models.Api.RequestModels;
using ObiletJourneySearch.Models.ViewModels;
using ObiletJourneySearch.Services.Abstract;

namespace ObiletJourneySearch.Controllers
{
    public class JourneyController : Controller
    {
        private readonly IObiletService _obiletService;

        public JourneyController(IObiletService obiletService)
        {
            _obiletService = obiletService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int originId, int destinationId, DateTime departureDate)
        {
            var session = await _obiletService.CreateSessionAsync();
            if (session is null)
                return View("Error");

            var request = new JourneyRequest
            {
                Data = new JourneyRequest.DataModel
                {
                    OriginId = originId,
                    DestinationId = destinationId,
                    DepartureDate = departureDate.ToString("yyyy-MM-dd")
                },
                DeviceSession = new JourneyRequest.DeviceSessions
                {
                    SessionId = session.SessionId,
                    DeviceId = session.DeviceId
                },
                Date = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss"),
                Language = "tr-TR"
            };

            var journeys = await _obiletService.GetJourneysAsync(request);
            var locations = await _obiletService.GetBusLocationsAsync(session.SessionId, session.DeviceId);
            var locationMap = locations.ToDictionary(x => x.Id);

            var origin = locationMap.GetValueOrDefault(originId);
            var destination = locationMap.GetValueOrDefault(destinationId);

            var viewModel = new JourneyViewModel
            {
                Journeys = journeys,
                OriginId = originId,
                DestinationId = destinationId,
                DepartureDate = departureDate,
                OriginName = origin?.Name ?? "Bilinmiyor",
                DestinationName = destination?.Name ?? "Bilinmiyor"
            };

            return View(viewModel);
        }
    }

}
