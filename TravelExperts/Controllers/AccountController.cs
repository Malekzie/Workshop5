using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;
using TravelExperts.DataAccess.Models;
using TravelExperts.DataAccess.Service;
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
            var customerIdClaim = User.FindFirst("CustomerId");
            if (customerIdClaim == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var customerId = int.Parse(customerIdClaim.Value);
            var customer = await _unitOfWork.Users.GetCustomerByID(customerId);

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(AccountVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var customerIdClaim = User.FindFirst("CustomerId");
            if (customerIdClaim == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var customerId = int.Parse(customerIdClaim.Value);
            var customer = await _unitOfWork.Users.GetCustomerByID(customerId);

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

 
        public async Task<IActionResult> History()
        {
            Claim customerIdClaim = User.FindFirst("CustomerId");
            if (customerIdClaim == null)
            {
                // Handle the case where the customer ID claim is not found
                return RedirectToAction("Login", "Account");
            }

            var customerId = int.Parse(customerIdClaim.Value);

            var accountHistoryVM = new AccountHistoryVM
            {
                Customers = _unitOfWork.Customers.GetAccount(customerId),
                Bookings = await _unitOfWork.Bookings.GetOrderHistory(customerId)
            };
            ViewBag.TotalPrice = new decimal();
            foreach (var account in accountHistoryVM.Bookings)
            {
                ViewBag.TotalPrice += _unitOfWork.Packages.GetPackagePrice((int)account.PackageId);
            }

            ViewBag.Package1Price = _unitOfWork.Packages.GetPackagePrice(1);
            ViewBag.Package2Price = _unitOfWork.Packages.GetPackagePrice(2);
            ViewBag.Package3Price = _unitOfWork.Packages.GetPackagePrice(3);
            ViewBag.Package4Price = _unitOfWork.Packages.GetPackagePrice(4);

            return View(accountHistoryVM);
        }
    }
}
