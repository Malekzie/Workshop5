using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelExperts.DataAccess.Models;
using TravelExperts.DataAccess.Service.IService;
using TravelExperts.Models.ViewModel;
using TravelExperts.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var packages = await _unitOfWork.Packages.GetAllAsync();
            var cards = new List<CardVM>();

            var imageMap = new Dictionary<string, string>
            {
                { "Caribbean New Year", "img/Caribbean3.jpg" },
                { "Polynesian Paradise", "img/Hawaii.jpg" },
                { "Asian Expedition", "img/Japan.jpg" },
                { "European Vacation", "img/Paris.jpg" }
            };

            foreach (var p in packages)
            {
                cards.Add(new CardVM
                {
                    Name = p.PkgName,
                    ImageUrl = imageMap.ContainsKey(p.PkgName) ? imageMap[p.PkgName] : "img/default.jpg",
                    Desc = p.PkgDesc
                });
            }

            return View(cards);
        }

        [Authorize]
        public async Task<IActionResult> BuyPackage(int packageId)
        {
            var tripTypes = StaticDefinition.GetTripTypes();

            var viewModel = new BuyPackageViewModel
            {
                PackageId = packageId,
                TripTypes = tripTypes,
                Booking = new Booking { PackageId = packageId }
            };

            ViewData["Title"] = "Buy package";
            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> BuyPackage(BuyPackageViewModel model)
        {
            var customerIdClaim = User.FindFirst("CustomerId");
            if (customerIdClaim == null)
            {
                ModelState.AddModelError("", "Customer ID not found.");
                model.TripTypes = StaticDefinition.GetTripTypes();
                return View(model);
            }

            var customerId = int.Parse(customerIdClaim.Value);

            if (!ModelState.IsValid)
            {
                model.TripTypes = StaticDefinition.GetTripTypes();
                return View(model);
            }

            try
            {
                var newBooking = new Booking
                {
                    BookingDate = DateTime.Now,
                    BookingNo = BookingUtil.GenerateBookingNumber(),
                    TravelerCount = model.Booking.TravelerCount,
                    CustomerId = customerId, // Set CustomerId from claims
                    TripTypeId = model.Booking.TripTypeId,
                    PackageId = model.PackageId
                };

                _unitOfWork.Bookings.Add(newBooking);
                await _unitOfWork.Save();
                return RedirectToAction("History", "Account");
            }
            catch
            {
                ModelState.AddModelError("", "An error occurred while processing your request.");
            }

            model.TripTypes = StaticDefinition.GetTripTypes();
            return View(model);
        }
    }
}
