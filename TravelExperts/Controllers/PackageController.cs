using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TravelExperts.DataAccess.Data;
using TravelExperts.DataAccess.Models;
using TravelExperts.Models;
using TravelExperts.Models.ViewModel;
using TravelExperts.DataAccess;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

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
            Package package1 = _context.Packages.First(p => p.PackageId == 1);
            Package package2 = _context.Packages.First(p => p.PackageId == 2);
            Package package3 = _context.Packages.First(p => p.PackageId == 3);
            Package package4 = _context.Packages.First(p => p.PackageId == 4);
            //IndexPackageViewModel indexPackageViewModel = new IndexPackageViewModel();
            var cards = new List<CardVM>
            {

                new CardVM
                {
                    Name = package1.PkgName,
                    ImageUrl = "img/Caribbean3.jpg",
                    Desc = package1.PkgDesc,              
                },
                new CardVM
                {
                    Name = package2.PkgName,
                    ImageUrl = "img/Hawaii.jpg",
                    Desc = package2.PkgDesc,
                },
                new CardVM
                {
                    Name = package3.PkgName,
                    ImageUrl = "img/Japan.jpg",
                    Desc = package3.PkgDesc,
                },
                new CardVM
                {
                    Name = package4.PkgName,
                    ImageUrl = "img/Paris.jpg",
                    Desc = package4.PkgDesc,
                }
            };
            
            return View(cards);
        }

        public IActionResult BuyPackage(int packageId)
        {
            List<TripType> tripTypeLabels = new List<TripType>
            {
                new TripType{ ID = "B", TripTypeName = "Business" },
                new TripType{ ID = "L", TripTypeName = "Leisure" },
                new TripType{ ID = "G", TripTypeName = "Group" }
            };
            var tripTypes = new SelectList(tripTypeLabels, "ID", "TripTypeName").ToList();
            ViewBag.TripTypes = tripTypes;
  
            var package = PackageManager.GetPackageByID(_context, packageId);
            ViewBag.PkgName = package.PkgName;
            ViewBag.PkgDesc = package.PkgDesc;
            ViewBag.PackageId = package.PackageId;
            //ViewBag.PkgName = PackageManager.GetPackageName(PackageManager.GetPackage(_context, 1));

            if (package == null)
            {
                return NotFound();
            }


            return View();
        }

        [HttpPost]
        public ActionResult BuyPackage(IFormCollection collection)
        {
            try
            {
                BookingManager.AddBooking(_context, new Booking
                {

                    BookingDate = DateTime.Now,
                    BookingNo = "123456",
                    TravelerCount = Convert.ToDouble(collection["TravelerCount"]),
                    CustomerId = 143,
                    TripTypeId = collection["TripTypeId"],
                    PackageId = 1

                });
                return RedirectToAction(nameof(Index));
            }
            catch
            {

                return View();
            }
        }
    }
}
