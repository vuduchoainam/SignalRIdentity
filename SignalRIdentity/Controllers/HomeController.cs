using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalRIdentity.Data;
using SignalRIdentity.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace SignalRIdentity.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser != null)
                {
                    ViewBag.CurrentUserName = currentUser.UserName; // Đặt giá trị cho ViewBag
                }
            }

            if (_context != null)
            {
                var messages = await _context.Messages.ToListAsync();
                return View(messages);
            }

            // Handle the case when _context is null (maybe log or return an appropriate response).
            return NotFound();
        }

        public async Task<IActionResult> Create(Message message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var sender = await _userManager.GetUserAsync(User);
                    var currentUser = await _userManager.GetUserAsync(User);
                    message.UserName = currentUser.UserName;
                    message.UserID = sender.Id;
                    await _context.Messages.AddAsync(message);
                    await _context.SaveChangesAsync();
                    return Ok("Message sent successfully");
                }
                return Error();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("Error sending message:", ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}