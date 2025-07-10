using DBAcess;
using DBAcess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http;
using ViewModel.Auth.User;
namespace User.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly HistoryMadeSimpleContext _context;

        public ProfileController(IHttpClientFactory httpClientFactory, UserManager<ApplicationUser> userManager, HistoryMadeSimpleContext context)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var info = await _context.Users.FirstOrDefaultAsync(u => u.UserId == user.Id);

            var model = new ProfileViewModel
            {
                UserId = user.Id,
                FullName = info?.FullName ?? "",
                Email = user.Email,
                UserName = user.UserName,
                SchoolName = info?.SchoolName ?? "",
            };

            return View(model);
        }
    }

}
