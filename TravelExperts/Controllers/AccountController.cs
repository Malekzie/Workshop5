using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using TravelExperts.DataAccess.Models;
using TravelExperts.DataAccess.Service.IService;
using Microsoft.AspNetCore.Authorization;

namespace TravelExperts.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var customer = _unitOfWork.Customers.GetCustomerByUsernameAndPassword(username, password);
            if (customer != null)
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                // Add more claims if needed
            };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    // ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(string username, string password, string firstName, string lastName, string address, string city, string prov, string postal, string country, string homePhone, string busPhone, string email)
        {
            var customer = new Customer
            {
                Username = username,
                Password = password, // Note: In a real application, passwords should be hashed
                CustFirstName = firstName,
                CustLastName = lastName,
                CustAddress = address,
                CustCity = city,
                CustProv = prov,
                CustPostal = postal,
                CustCountry = country,
                CustHomePhone = homePhone,
                CustBusPhone = busPhone,
                CustEmail = email
            };

            _unitOfWork.Customers.Add(customer);
            _unitOfWork.Save();

            return RedirectToAction("Login", "Account");
        }

        public IActionResult Index => View();

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
