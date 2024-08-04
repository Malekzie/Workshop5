using Microsoft.AspNetCore.Mvc;
using TravelExperts.DataAccess.Models;
using TravelExperts.DataAccess.Service.IService;
using TravelExperts.Models.ViewModel;
using TravelExperts.Utils;
// Written by: Ben Wood
// Refactored by: Robbie Soriano, Aiden Giesbrecht

namespace TravelExperts.Controllers
{
    public class PackageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PackageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var package = await _unitOfWork.Packages.GetAllAsync();
            var cards = new List<CardVM>();

            var imageMap = new Dictionary<string, string>
            {

                { "Caribbean New Year", "img/Caribbean3.jpg" },
                { "Polynesian Paradise", "img/Hawaii.jpg" },
                { "Asian Expedition", "img/Japan.jpg" },
                { "European Vacation", "img/Paris.jpg" }
            };

            foreach (var p in package)
            {
                cards.Add(new CardVM
                {
                    Name = p.PkgName,
                    ImageUrl = imageMap.ContainsKey(p.PkgName) ? imageMap[p.PkgName] : "img/default.jpg", // Default image if no match
                    Desc = p.PkgDesc
                });
            }

            return View(cards);
        }

        public async Task<IActionResult> BuyPackage(int packageId)
        {
            List<TripType> tripTypeLabels = new List<TripType>
            {
                new TripType { ID = "B", TripTypeName = "Business" },
                new TripType { ID = "L", TripTypeName = "Leisure" },
                new TripType { ID = "G", TripTypeName = "Group" }
            };

            var packages = await _unitOfWork.Packages.GetAllAsync();
            var package = packages.FirstOrDefault(p => p.PackageId == packageId);
            if (package == null)
            {
                return NotFound();
            }

            var viewModel = new BuyPackageViewModel
            {
                Package = package,
                TripTypes = tripTypeLabels,
                Booking = new Booking()
            };
            ViewBag.PkgName = package.PkgName;
            ViewBag.PkgDesc = package.PkgDesc;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult BuyPackage(BuyPackageViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newBooking = new Booking
                    {
                        BookingDate = DateTime.Now,
                        BookingNo = BookingUtil.GenerateBookingNumber(),
                        TravelerCount = model.Booking.TravelerCount,
                        CustomerId = 143, // Replace with actual customer ID
                        TripTypeId = model.Booking.TripTypeId,
                        PackageId = model.Package.PackageId
                    };

                    _unitOfWork.Bookings.Add(newBooking);
                    _unitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Log the exception (ex) if necessary
                    ModelState.AddModelError("", "An error occurred while processing your request.");
                }
            }

            // If we got this far, something failed; re-populate TripTypes for the view
            model.TripTypes = StaticDefinition.GetTripTypes();

            return View(model);
        }
    }
    
}
