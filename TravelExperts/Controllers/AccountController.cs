using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TravelExperts.DataAccess.Models;
using TravelExperts.DataAccess.Service.IService;
using TravelExperts.Models;
using TravelExperts.Models.ViewModel;

namespace TravelExperts.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = _unitOfWork.Users.GetUser(username, password);
            var customer = await _unitOfWork.Users.GetCustomerByID(user.UserId);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim("FullName", customer.CustFirstName + " " + customer.CustLastName),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View();
        }

        public IActionResult Register(string returnUrl = null)
        {
            var model = new RegisterVM
            {
                Input = new RegisterVM.InputModel(),
                ProvinceList = GetProvinces(),
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterVM model, string returnUrl = null)
        {
            model.ReturnUrl = returnUrl;
            if (ModelState.IsValid)
            {

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
                _unitOfWork.Save(); // Ensure changes are saved to the database

                // Create the user after the customer has been created and has an ID
                var user = new User
                {
                    Username = model.Input.Username,
                    Password = model.Input.Password,
                    CustomerId = customer.CustomerId // Ensure this is set after the customer is saved
                };

                _unitOfWork.Users.RegisterUser(user);
                _unitOfWork.Save(); // Save changes again to register the user

                return RedirectToAction("Login", "Account");
            }

            // If we get here, something went wrong, redisplay form
            model.ProvinceList = GetProvinces();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        private Dictionary<string, string> GetProvinces()
        {
            return new Dictionary<string, string>
            {
                { "AB", "Alberta" },
                { "BC", "British Columbia" },
                { "MB", "Manitoba" },
                { "NB", "New Brunswick" },
                { "NL", "Newfoundland and Labrador" },
                { "NS", "Nova Scotia" },
                { "ON", "Ontario" },
                { "PE", "Prince Edward Island" },
                { "QC", "Quebec" },
                { "SK", "Saskatchewan" },
                { "NT", "Northwest Territories" },
                { "NU", "Nunavut" },
                { "YT", "Yukon" }
            };
        }
    }
}
