using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ObiletJourneySearch.Models;
using ObiletJourneySearch.Models.ViewModels;
using ObiletJourneySearch.Services.Abstract;

namespace ObiletJourneySearch.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IObiletService _obiletService;

        public HomeController(ILogger<HomeController> logger, IObiletService obiletService)
        {
            _logger = logger;
            _obiletService = obiletService;
        }

        public async Task<IActionResult> Index(int? originId, int? destinationId, DateTime? departureDate)
        {
            var session = await _obiletService.CreateSessionAsync();
            if (session is null)
                return View("Error");

            var locations = await _obiletService.GetBusLocationsAsync(session.SessionId, session.DeviceId, null);

            var viewModel = new IndexViewModel
            {
                Locations = locations,
                SelectedOriginId = originId,
                SelectedDestinationId = destinationId,
                SelectedDate = departureDate ?? DateTime.Today.AddDays(1)
            };

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
