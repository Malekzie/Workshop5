using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TravelExperts.DataAccess.Data;
using TravelExperts.DataAccess.Models;
using TravelExperts.Models;
using TravelExperts.Models.ViewModel;

namespace TravelExperts.Controllers
{
    public class PackageController : Controller
    {
        private TravelExpertsContext _context { get; set; }

        public PackageController(TravelExpertsContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cards = new List<CardVM>
            {
                new CardVM
                {
                    Name = "Card 1",
                    ImageUrl = "img/Caribbean3.jpg",
                    Desc = "This is a card"
                },
                new CardVM
                {
                    Name = "Card 2",
                    ImageUrl = "img/Hawaii.jpg",
                    Desc = "This is a card"
                },
                new CardVM
                {
                    Name = "Card 3",
                    ImageUrl = "img/Japan.jpg",
                    Desc = "This is a card"
                },
                new CardVM
                {
                    Name = "Card 4",
                    ImageUrl = "img/Paris.jpg",
                    Desc = "This is a card"
                }
            };
            return View(cards);
        }

        public IActionResult BuyPackage()
        {
            var package = Package.GetPackage();
            
            return View();
        }
    }
}
