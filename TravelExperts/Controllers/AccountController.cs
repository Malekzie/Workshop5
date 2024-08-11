using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using TravelExperts.DataAccess.Models;
using TravelExperts.DataAccess.Service.IService;
using TravelExperts.Models;
using TravelExperts.Models.ViewModel;
using TravelExperts.Utils;

namespace TravelExperts.Controllers
{
    /// <summary>
    /// Manages user account-related actions including login, logout, and registration.
    /// </summary>
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the AccountController class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work to manage data access.</param>
        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Displays the index page.
        /// </summary>
        public async Task<IActionResult> Index()
        {
            try
            {
                var customerIdClaim = User.FindFirst("CustomerId");
                if (customerIdClaim == null)
                {
                    return RedirectToAction("Login", "Auth");
                }

                var customerId = int.Parse(customerIdClaim.Value);
                var customer = await _unitOfWork.Customers.GetByIdAsync(customerId);

                if (customer == null)
                {
                    return NotFound("Customer not found.");
                }

                var accountVM = new AccountVM
                {
                    FirstName = customer.CustFirstName,
                    LastName = customer.CustLastName,
                    BusinessPhone = customer.CustBusPhone,
                    HomePhone = customer.CustHomePhone,
                    Address = customer.CustAddress,
                    City = customer.CustCity,
                    Province = customer.CustProv,
                    PostalCode = customer.CustPostal,
                    Country = customer.CustCountry,
                };

                return View(accountVM);
            }
            catch (Exception ex)
            {
                // Log the exception (logging not implemented here for simplicity)
                ModelState.AddModelError("", "An error occurred while loading the account details.");
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(AccountVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var customerIdClaim = User.FindFirst("CustomerId");
                if (customerIdClaim == null)
                {
                    return RedirectToAction("Login", "Auth");
                }

                var customerId = int.Parse(customerIdClaim.Value);
                var customer = await _unitOfWork.Customers.GetByIdAsync(customerId);

                if (customer == null)
                {
                    return NotFound("Customer not found.");
                }

                customer.CustFirstName = model.FirstName;
                customer.CustLastName = model.LastName;
                customer.CustBusPhone = model.BusinessPhone;
                customer.CustHomePhone = model.HomePhone;
                customer.CustAddress = model.Address;
                customer.CustCity = model.City;
                customer.CustProv = model.Province;
                customer.CustPostal = model.PostalCode;
                customer.CustCountry = model.Country;

                _unitOfWork.Customers.Update(customer);
                await _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log the exception (logging not implemented here for simplicity)
                ModelState.AddModelError("", "An error occurred while updating the account details.");
                return View(model);
            }
        }

        /**
         The history page made use of viewmodels in order to allow multiple database tables to exist
         in the same model. I did use a viewbag for calculated values to be passed to the page, but
         from some research it seems that most devs are avoiding viewbag in favour of ensuring all
         data exists in the viewmodel. While null checks do exist thoughout the code, validation becomes
         much simpler if the null value is disallowed in the database from the get go. 
         **/
        public async Task<IActionResult> History() 
        {
            try
            {
                Claim customerIdClaim = User.FindFirst("CustomerId");
                if (customerIdClaim == null)
                {
                    return RedirectToAction("Login", "Auth");
                }

                var customerId = int.Parse(customerIdClaim.Value);

                var customer = _unitOfWork.Customers.GetAccount(customerId);
                var bookings = await _unitOfWork.Bookings.GetOrderHistory(customerId);

                if (customer == null || bookings == null)
                {
                    return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                }

                var accountHistoryVM = new AccountHistoryVM
                {
                    Customers = customer,
                    Bookings = bookings
                };

                // Create a dictionary to store package prices by package ID
                var packagePrices = new Dictionary<int, decimal>();
                var packageNames = new Dictionary<int, string>();

                decimal totalPrice = 0;
                foreach (var booking in bookings)
                {
                    var package = await _unitOfWork.Packages.GetByIdAsync(booking.PackageId);
                    if (package != null)
                    {
                        if (!packagePrices.ContainsKey(booking.PackageId))
                        {
                            packagePrices[booking.PackageId] = package.PkgBasePrice;
                            packageNames[booking.PackageId] = package.PkgName;
                        }
                        totalPrice += package.PkgBasePrice; // Assuming PkgBasePrice is nullable
                    }
                }

                ViewBag.TotalPrice = totalPrice;
                ViewBag.PackagePrices = packagePrices;
                ViewBag.PackageNames = packageNames;

                return View(accountHistoryVM);
            }
            catch (Exception ex)
            {
                // Log the exception (logging not implemented here for simplicity)
                ModelState.AddModelError("", "An error occurred while loading the order history.");
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }



        public async Task<IActionResult> EditAccount()
        {
            try
            {
                var customerIdClaim = User.FindFirst("CustomerId");
                if (customerIdClaim == null)
                {
                    return RedirectToAction("Login", "Auth");
                }

                var customerId = int.Parse(customerIdClaim.Value);
                var customer = await _unitOfWork.Customers.GetByIdAsync(customerId);

                if (customer == null)
                {
                    return NotFound("Customer not found.");
                }

                var model = new CredentialsVM
                {
                    Email = customer.CustEmail,
                    Username = customer.Username
                };

                return View(model);
            }
            catch (Exception ex)
            {
                // Log the exception (logging not implemented here for simplicity)
                ModelState.AddModelError("", "An error occurred while loading the edit account page.");
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAccount(CredentialsVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var customerIdClaim = User.FindFirst("CustomerId");
                if (customerIdClaim == null)
                {
                    return RedirectToAction("Login", "Auth");
                }

                var customerId = int.Parse(customerIdClaim.Value);
                var customer = await _unitOfWork.Customers.GetByIdAsync(customerId);

                if (customer == null)
                {
                    return NotFound("Customer not found.");
                }

                // Update email and password
                customer.CustEmail = model.Email;
                customer.Password = model.Password;
                customer.Username = model.Username;

                _unitOfWork.Customers.Update(customer);
                await _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
               
                ModelState.AddModelError("", "An error occurred while updating the account details.");
                return View(model);
            }
        }
    }
}
