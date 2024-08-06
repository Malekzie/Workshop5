using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
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

        /// <summary>
        /// Displays the login page.
        /// </summary>
        [HttpGet]
        public IActionResult Login() => View();

        /// <summary>
        /// Handles the login POST request.
        /// </summary>
        /// <param name="username">The username entered by the user.</param>
        /// <param name="password">The password entered by the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the IActionResult.</returns>
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = _unitOfWork.Users.GetUser(username);
            if (user == null || user.Password != password)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View();
            }

            var customer = await _unitOfWork.Users.GetCustomerByID(user.UserId);

            if (customer != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim("FullName", customer.CustFirstName + " " + customer.CustLastName),
                    new Claim("CustomerId", customer.CustomerId.ToString()) // Add CustomerId claim
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties { IsPersistent = true };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                // Set customer ID in a cookie
                HttpContext.Response.Cookies.Append("CustomerId", customer.CustomerId.ToString(), new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(30), // Set cookie to expire in 30 days
                    IsEssential = true, // Required for GDPR compliance
                    HttpOnly = true // Ensures the cookie is accessible only to the server
                });

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View();
        }



        /// <summary>
        /// Displays the registration page.
        /// </summary>
        /// <param name="returnUrl">The URL to return to after registration.</param>
        /// <returns>The registration view.</returns>
        public IActionResult Register(string returnUrl = null)
        {
            var model = new RegisterVM
            {
                Input = new RegisterVM.InputModel(),
                ProvinceList = StaticDefinition.GetProvinces(),
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        /// <summary>
        /// Handles the registration POST request.
        /// </summary>
        /// <param name="model">The registration view model containing user input.</param>
        /// <param name="returnUrl">The URL to return to after registration.</param>
        /// <returns>An IActionResult representing the result of the action.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterVM model, string returnUrl = null)
        {
            model.ReturnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                var existingUser = _unitOfWork.Users.GetUser(model.Input.Username);
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "Username already exists.");
                    model.ProvinceList = StaticDefinition.GetProvinces();
                    return View(model);
                }

                var customer = new Customer
                {
                    CustFirstName = model.Input.CustFirstName,
                    CustLastName = model.Input.CustLastName,
                    CustAddress = model.Input.CustAddress,
                    CustCity = model.Input.CustCity,
                    CustProv = model.Input.CustProv,
                    CustPostal = model.Input.CustPostal,
                    CustCountry = model.Input.CustCountry,
                    CustHomePhone = model.Input.CustHomePhone,
                    CustBusPhone = model.Input.CustBusPhone,
                    CustEmail = model.Input.Email
                };

                _unitOfWork.Customers.RegisterCustomer(customer);

                var user = new User
                {
                    Username = model.Input.Username,
                    Password = model.Input.Password,
                    CustomerId = customer.CustomerId
                };

                _unitOfWork.Users.RegisterUser(user);
                _unitOfWork.Save();

                return RedirectToAction("Login", "Account");
            }

            ModelState.AddModelError("", "Invalid registration attempt.");
            model.ProvinceList = StaticDefinition.GetProvinces();
            return View(model);
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

        /// <summary>
        /// Handles the logout POST request.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the IActionResult.</returns>
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
