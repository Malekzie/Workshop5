using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelExperts.DataAccess.Models;
using TravelExperts.DataAccess.Service.IService;
using TravelExperts.Models.ViewModel;
using TravelExperts.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Controller for package-related actions
//Writen by Ben Wood


namespace TravelExperts.Controllers
{

    public class PackageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PackageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /***
         * Display all packages
         */
        public async Task<IActionResult> Index()
        {
            // Get all packages
            var packages = await _unitOfWork.Packages.GetAllAsync();
            // Create a list of CardVM to display packages
            var cards = new List<CardVM>();

            // Image mapping for each package
            var imageMap = new Dictionary<string, string>
            {
                { "Caribbean New Year", "img/Caribbean3.jpg" },
                { "Polynesian Paradise", "img/Hawaii.jpg" },
                { "Asian Expedition", "img/Japan.jpg" },
                { "European Vacation", "img/Paris.jpg" }
            };

            // Add each package to the list
            foreach (var p in packages)
            {
                // Create a new CardVM object
                cards.Add(new CardVM
                {
                    // Assign values to the properties
                    Id = p.PackageId,
                    Name = p.PkgName,
                    ImageUrl = imageMap.ContainsKey(p.PkgName) ? imageMap[p.PkgName] : "img/default.jpg",
                    Desc = p.PkgDesc
                });
            }

            // Return the view with the list of packages
            return View(cards);
        }

        /// <summary>
        /// When the user clicks on a package, they will be taken to the buy package page
        /// sets the packageId and tripTypes as well as a booking object for the view model.
        /// Also checks if the user is logged in
        /// </summary>
        /// <param name="packageId"></param>
        /// <returns>the buy package viewModel</returns>
        [Authorize]
        public async Task<IActionResult> BuyPackage(int packageId)
        {
            // Get the trip types
            var tripTypes = StaticDefinition.GetTripTypes();

            // Create a new view model
            var viewModel = new BuyPackageViewModel
            {
                // Assign values to the properties
                PackageId = packageId,
                TripTypes = tripTypes,
                Booking = new Booking { PackageId = packageId }
            };

            // Return the view with the view model
            return View(viewModel);
        }


        /// <summary>
        /// When the user clicks on the buy button, the package will be added to the user's booking history
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Model</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> BuyPackage(BuyPackageViewModel model)
        {
            // Get the customer ID from the claims
            var customerIdClaim = User.FindFirst("CustomerId");
            // If the customer ID is not found, return an error
            if (customerIdClaim == null)
            {
                // Add an error to the model state
                ModelState.AddModelError("", "Customer ID not found.");
                // Return the view with the model
                model.TripTypes = StaticDefinition.GetTripTypes();
                return View(model);
            }
            // Parse the customer ID
            var customerId = int.Parse(customerIdClaim.Value);

            // If the model state is not valid, return the view with the model
            if (!ModelState.IsValid)
            {
                // Return the view with the model
                model.TripTypes = StaticDefinition.GetTripTypes();
                return View(model);
            }

            // Try to add the booking
            try
            {
                // Create a new booking
                var newBooking = new Booking
                {
                    // Assign values to the properties
                    BookingDate = DateTime.Now,
                    BookingNo = BookingUtil.GenerateBookingNumber(),
                    TravelerCount = model.Booking.TravelerCount,
                    CustomerId = customerId,
                    TripTypeId = model.Booking.TripTypeId,
                    PackageId = model.PackageId
                };

                // Add the booking to the database
                _unitOfWork.Bookings.Add(newBooking);
                // Save the changes
                await _unitOfWork.Save();
                // Redirect to the history page
                return RedirectToAction("History", "Account");
            }
            // If an error occurs, return an error
            catch
            {
                // Add an error to the model state
                ModelState.AddModelError("", "An error occurred while processing your request.");
            }

            // Return the view with the model
            model.TripTypes = StaticDefinition.GetTripTypes();
            return View(model);
        }
    }
}
