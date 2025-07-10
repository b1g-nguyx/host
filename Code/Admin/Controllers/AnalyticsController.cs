using DBAcess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private readonly HistoryMadeSimpleContext _context;
        public AnalyticsController(HistoryMadeSimpleContext context)
        {
            _context = context;
        }

        [HttpGet("peak-hours")]
        public async Task<IActionResult> GetPeakLoginHours()
        {
            var startDate = DateTime.UtcNow.Date.AddDays(-30);

            var result = await _context.UserSessions
                .Where(s => s.LoginTime >= startDate)
                .GroupBy(s => s.LoginTime.Hour)
                .Select(g => new
                {
                    Hour = g.Key,
                    Logins = g.Count()
                })
                .OrderBy(g => g.Hour)
                .ToListAsync();

            return Ok(result);
        }


        [HttpGet("account-created")]
        public async Task<IActionResult> GetAccountCreatedStats()
        {
            var startDate = DateTime.UtcNow.AddMonths(-1).Date;

            var result = await _context.Users
                .Include(u => u.ApplicationUser)
                .Where(u => u.ApplicationUser.CreatedAt >= startDate)
                .GroupBy(u => u.ApplicationUser.CreatedAt.Date)
                .Select(g => new
                {
                    Date = g.Key.ToString("yyyy-MM-dd"),
                    Count = g.Count()
                }).ToListAsync();

            return Ok(result);
        }

        [HttpGet("active-users")]
        public async Task<IActionResult> GetActiveUserStats()
        {
            var startDate = DateTime.UtcNow.AddMonths(-1).Date;

            var result = await _context.UserSessions
                .Where(s => s.LoginTime >= startDate)
                .GroupBy(s => s.LoginTime.Date)
                .Select(g => new
                {
                    Date = g.Key.ToString("yyyy-MM-dd"),
                    ActiveUsers = g.Select(x => x.UserId).Distinct().Count()
                }).ToListAsync();

            return Ok(result);
        }
    }
}
