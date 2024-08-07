using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TravelExperts.DataAccess.Service.IService;
using TravelExperts.Models.ViewModel;
using TravelExperts.Utils;
using TravelExperts.Models;

namespace TravelExperts.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {
                var customer = await _unitOfWork.Customers.ValidateCustomer(username, password);

                if (customer == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim("FullName", customer.CustFirstName + " " + customer.CustLastName),
                    new Claim("CustomerId", customer.CustomerId.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties { IsPersistent = true };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                // Set customer ID in a cookie
                HttpContext.Response.Cookies.Append("CustomerId", customer.CustomerId.ToString(), new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(30),
                    IsEssential = true,
                    HttpOnly = true
                });

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                // Log the exception (logging not implemented here for simplicity)
                ModelState.AddModelError("", "An error occurred while processing your request.");
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        public IActionResult Register(string returnUrl = null)
        {
            try
            {
                var model = new RegisterVM
                {
                    Input = new RegisterVM.InputModel(),
                    ProvinceList = StaticDefinition.GetProvinces(),
                    ReturnUrl = returnUrl
                };
                return View(model);
            }
            catch (Exception ex)
            {
                // Log the exception (logging not implemented here for simplicity)
                ModelState.AddModelError("", "An error occurred while loading the registration page.");
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterVM model, string returnUrl = null)
        {
            try
            {
                model.ReturnUrl = returnUrl;
                if (ModelState.IsValid)
                {
                    var existingCustomer = _unitOfWork.Customers.GetCustomerByUsername(model.Input.Username);
                    if (existingCustomer != null)
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
                        CustEmail = model.Input.Email,
                        Username = model.Input.Username,
                        Password = model.Input.Password
                    };

                    _unitOfWork.Customers.RegisterCustomer(customer);
                    _unitOfWork.Save();

                    return RedirectToAction("Login", "Auth");
                }

                ModelState.AddModelError("", "Invalid registration attempt.");
                model.ProvinceList = StaticDefinition.GetProvinces();
                return View(model);
            }
            catch (Exception ex)
            {
                // Log the exception (logging not implemented here for simplicity)
                ModelState.AddModelError("", "An error occurred while processing your registration.");
                model.ProvinceList = StaticDefinition.GetProvinces();
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Login", "Auth");
            }
            catch (Exception ex)
            {
                // Log the exception (logging not implemented here for simplicity)
                ModelState.AddModelError("", "An error occurred while logging out.");
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
