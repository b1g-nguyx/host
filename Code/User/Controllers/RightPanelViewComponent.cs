using DBAcess;
using DBAcess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ViewModel.DailyTaskProgress;

namespace User.Controllers
{
    public class RightPanelViewComponent : ViewComponent
    {
        private readonly HistoryMadeSimpleContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public RightPanelViewComponent(HistoryMadeSimpleContext context, IHttpContextAccessor httpContextAccessor , UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (!User.Identity.IsAuthenticated)
                return View("Guest"); // Chưa đăng nhập

            // Lấy user từ UserManager
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
                return View("Guest");

            var userId = user.Id;
            var userProfile = await _context.Users!.FirstOrDefaultAsync(u => u.UserId == userId);
            var fullName = userProfile!.FullName ?? "Người dùng";

            // Tính tiến trình (giả lập số liệu, bạn có thể thay bằng truy vấn thực)
            int expEarned = await _context.UserAnswers
            .CountAsync(x => x.UserID == userProfile.Id && x.IsCorrect == true);
            int expTarget = await _context.Questions.CountAsync(); // Mục tiêu tùy bạn định nghĩa

            var highScoreLessons = await _context.Lessons
             .Where(lesson => _context.Questions
                .Any(q => q.LessonID == lesson.Id) &&
                _context.UserAnswers
             .Where(ua => ua.UserID == userProfile.Id)
             .Where(ua => ua.IsCorrect)
             .Count(ua => _context.Questions.Any(q => q.Id == ua.QuestionID && q.LessonID == lesson.Id)) * 1.0
             /
             _context.Questions.Count(q => q.LessonID == lesson.Id) >= 0.9
             )
             .CountAsync();
            int highScoreTarget = 2; 


            int studyMinutes = await _context.LessonProgress
            .Where(lp => lp.UserID == userProfile.Id)
            .SumAsync(lp => lp.ProgressPercentage) / 10; // mỗi 10% tính là 1 phút học chẳng hạn
            int studyTarget = 10;


            var model = new DailyTaskProgressViewModel
            {
                FullName = fullName,
                ExpProgress = Math.Min(100, expEarned * 100 / expTarget),
                HighScoreProgress = Math.Min(100, highScoreLessons * 100 / highScoreTarget),
                StudyTimeProgress = Math.Min(100, studyMinutes * 100 / studyTarget)
            };

            return View("Default", model);
        }

    }
}
