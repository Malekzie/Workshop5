using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TravelExperts.Models;

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
                    Name = "Card 1",
                    ImageUrl = "https://via.placeholder.com/150",
                    Desc = "This is a card"
                },
                new CardVM
                {
                    Name = "Card 2",
                    ImageUrl = "https://via.placeholder.com/150",
                    Desc = "This is a card"
                },
                new CardVM
                {
                    Name = "Card 3",
                    ImageUrl = "https://via.placeholder.com/150",
                    Desc = "This is a card"
                },
                new CardVM
                {
                    Name = "Card 4",
                    ImageUrl = "https://via.placeholder.com/150",
                    Desc = "This is a card"
                }
            };


            return View(cards);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
