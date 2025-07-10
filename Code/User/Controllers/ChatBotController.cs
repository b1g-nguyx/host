using DBAcess;
using DBAcess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using User.Services;

namespace User.Controllers
{
    public class ChatBotController : Controller
    {
        private readonly HistoryMadeSimpleContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly GroqService _groqService;
        public ChatBotController( HistoryMadeSimpleContext context, UserManager<ApplicationUser> userManager,  GroqService groqService)
        {
            _context = context;
            _userManager = userManager;
            _groqService = groqService;
        }

        [HttpPost]
        public async Task<IActionResult> Ask(string message)
        {
            // Gọi Azure OpenAI để lấy phản hồi
            //var reply = await _aiService.AskAsync(message);
            //var reply = await _openAiService.AskAsync(message);
            var reply = await _groqService.AskAsync(message);
            // Kiểm tra đăng nhập
            var currentUserId = _userManager.GetUserId(User);
            // Nếu có đăng nhập mới lưu
            if (!string.IsNullOrEmpty(currentUserId))
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == currentUserId);
                if (user != null)
                {
                    var content = new AIGeneratedContent
                    {
                        UserId = user.Id,
                        Prompt = message,
                        Response = reply,
                        CreatedDate = DateTime.Now,
                        ContentType = "chat"
                    };

                    _context.AIGeneratedContent.Add(content);
                    await _context.SaveChangesAsync();
                }
            }

            // Trả về phản hồi cho client, luôn gửi dù có đăng nhập hay không
            return Json(new { reply });
        }

    }

}
