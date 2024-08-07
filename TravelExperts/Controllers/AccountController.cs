using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TravelExperts.DataAccess.Models;
using TravelExperts.DataAccess.Service.IService;
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

                var accountHistoryVM = new AccountHistoryVM
                {
                    Customers = _unitOfWork.Customers.GetAccount(customerId),
                    Bookings = await _unitOfWork.Bookings.GetOrderHistory(customerId)
                };

                ViewBag.TotalPrice = accountHistoryVM.Bookings.Sum(account => _unitOfWork.Packages.GetPackagePrice((int)account.PackageId));

                for (int i = 1; i <= 4; i++)
                {
                    ViewBag[$"Package{i}Price"] = _unitOfWork.Packages.GetPackagePrice(i);
                }

                return View(accountHistoryVM);
            }
            catch (Exception ex)
            {
                // Log the exception (logging not implemented here for simplicity)
                ModelState.AddModelError("", "An error occurred while loading the order history.");
                return View("Error");
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
                    Email = customer.CustEmail
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
