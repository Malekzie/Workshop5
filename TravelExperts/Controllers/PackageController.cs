using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TravelExperts.DataAccess.Data;
using TravelExperts.DataAccess.Models;
using TravelExperts.Models;
using TravelExperts.Models.ViewModel;
using TravelExperts.DataAccess;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            //IndexPackageViewModel indexPackageViewModel = new IndexPackageViewModel();
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
            List<TripType> tripTypeLabels = new List<TripType> 
            { 
                new TripType{ ID = "B", TripTypeName = "Business" },
                new TripType{ ID = "L", TripTypeName = "Leisure" },
                new TripType{ ID = "G", TripTypeName = "Group" }
            };
            var tripTypes = new SelectList(tripTypeLabels, "ID", "TripTypeName").ToList();
            ViewBag.TripTypes = tripTypes;

            //indexPackageViewModel.cards = cards;
            //indexPackageViewModel.packages = PackageManager.GetPackages(_context);
            return View(cards);
        }

        public IActionResult BuyPackage()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult BuyPackage(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {

                return View();
            }
        }
    }
}
