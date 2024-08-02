using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TravelExperts.DataAccess.Data;
using TravelExperts.DataAccess.Models;
using TravelExperts.Models;
using TravelExperts.Models.ViewModel;

namespace TravelExperts.Controllers
{
    public class AccountController : Controller
    {
        private TravelExpertsContext _context { get; set; }

        public AccountController(TravelExpertsContext context)
        {
            _context = context;
        }

        public IActionResult History()
        {
            Customer customer = AccountManager.GetOrderHistory(_context);
            return View(customer);
        }
    }
}
