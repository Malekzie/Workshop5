using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TravelExperts.DataAccess.Models;
using TravelExperts.DataAccess.Service.IService;
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

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var customer = _unitOfWork.Customers.GetCustomerByUsernameAndPassword(username, password);
            if (customer != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username)
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

        [HttpGet]
        [Route("Register")]
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
        [Route("Register")]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterVM model, string returnUrl = null)
        {
            model.ReturnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                var customer = new Customer
                {
                    Username = model.Input.Email,
                    Password = model.Input.Password, // Password should be hashed in a real application
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

                return RedirectToAction("Login", "Account");
            }

            // If we get here, something went wrong, redisplay form

            model.ProvinceList = GetProvinces();
            return View(model);
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

        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
