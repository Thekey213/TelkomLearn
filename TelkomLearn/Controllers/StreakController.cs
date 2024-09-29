using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TelkomLearn.Data;
using TelkomLearn.Models;

namespace TelkomLearn.Controllers
{
    [Authorize]
    public class StreakController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public StreakController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var userStreak = _context.UserStreaks.FirstOrDefault(u => u.UserId == user.Id);
            if (userStreak == null)
            {
                userStreak = new UserStreak
                {
                    UserId = user.Id,
                    StreakDays = 0,
                    MaxStreakDays = 100
                };
                _context.UserStreaks.Add(userStreak);
                await _context.SaveChangesAsync();
            }

            // Create the StreakModel for view
            var model = new StreakModel
            {
                StreakDays = userStreak.StreakDays,
                MaxStreakDays = userStreak.MaxStreakDays
            };

            return View(model);
        }
    }
}
