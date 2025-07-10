using DBAcess;
using DBAcess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using ViewModel.Auth.User;
namespace User.APITest
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly HistoryMadeSimpleContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController( HistoryMadeSimpleContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] ProfileViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null) return NotFound();

            user.Email = model.Email;
            user.UserName = model.UserName;
            
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest("Không thể cập nhật.");

            var userInfo = await _context.Users.FirstOrDefaultAsync(u => u.UserId == user.Id);
            if (userInfo != null)
            {
                userInfo.SchoolName = model.SchoolName;
                userInfo.FullName = model.FullName;
                await _context.SaveChangesAsync();
            }

            return Ok(new { message = "Cập nhật thành công!" });
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest req)
        {
            var user = await _userManager.FindByIdAsync(req.UserId);
            if (user == null)
                return NotFound("Người dùng không tồn tại.");

            var result = await _userManager.ChangePasswordAsync(user, req.CurrentPassword, req.NewPassword);
            if (!result.Succeeded)
                return BadRequest("Sai mật khẩu hiện tại hoặc mật khẩu mới không hợp lệ.");

            return Ok(new { message = "Đổi mật khẩu thành công!" });
        }

        [HttpGet("Chart/{userId}")]
        public async Task<IActionResult> Chart(string userId)
        {
            DateTime now = DateTime.UtcNow;
            DateTime from = now.AddDays(-6).Date;

            var sessions = await _context.UserSessions
                .Where(x => x.UserId == userId && x.LoginTime >= from && x.LogoutTime != null)
                .ToListAsync();

            var result = sessions
                .GroupBy(s => s.LoginTime.Date)
                .Select(g => new
                {
                    date = g.Key.ToString("dd/MM"),
                    totalMinutes = g.Sum(s => (int)(s.LogoutTime.Value - s.LoginTime).TotalMinutes)
                })
                .OrderBy(x => DateTime.ParseExact(x.date, "dd/MM", null))
                .ToList();

            return Ok(result);
        }


    }


}
