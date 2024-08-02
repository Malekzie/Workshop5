using Microsoft.AspNetCore.Mvc;
using TravelExperts.Models.ViewModel;

namespace TravelExperts.Controllers
{
    public class PackageController : Controller
    {
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
            return View();
        }
    }
}
