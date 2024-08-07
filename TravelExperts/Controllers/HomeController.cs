using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TravelExperts.Models;
using TravelExperts.Models.ViewModel;

namespace TravelExperts.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var cards = new List<CardVM>
            {
                new CardVM
                {
                    Name = "Car Service",
                    ImageUrl = "svg/car.svg",
                    Desc = "In addition to our packages, we offer car rentals to ensure you have the freedom to explore your destination at your own pace. Whether it's a quick trip to the beach or a scenic drive through the countryside, our reliable vehicles will get you there comfortably."
                },
                new CardVM
                {
                    Name = "Tour Service",
                    ImageUrl = "svg/tour.svg",
                    Desc = "Enhance your travel experience with our high-quality tour guides. Our knowledgeable guides will introduce you to the rich history and hidden gems of your destination, ensuring you make the most out of your trip."
                },
                new CardVM
                {
                    Name = "Yacht / Boat Service",
                    ImageUrl = "svg/yacht.svg",
                    Desc = "Experience luxury on the water with our yacht and boat services. Whether you're looking to relax on a serene lake or sail across open seas, our well-equipped vessels offer the perfect escape for any adventurer."
                },
               new CardVM
               {
                    Name = "Travel Insurance",
                    ImageUrl = "svg/insurance.svg",
                    Desc = "Travel with peace of mind knowing you're covered. Our comprehensive travel insurance plans protect you from unexpected events, ensuring you can enjoy your trip without worry. From medical emergencies to trip cancellations, we've got you covered."
               }

            };


            return View(cards);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var model = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(model);
        }
    }
}