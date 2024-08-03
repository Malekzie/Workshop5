using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace TravelExperts.Controllers
{
    public class AccountController : Controller
    {

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            // Here you should validate the user credentials from your database
            // For example, let's assume any non-empty credentials are valid
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
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

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(string username, string password, string email, string phone)
        {
            // Create a new user and save to the database
            var user = new User
            {
                Username = username,
                Password = password, // Note: In a real application, passwords should be hashed
                Email = email,
                Phone = phone
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Login", "Account");
        }
    }
}
