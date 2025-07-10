using DBAcess;
using DBAcess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace User.Controllers
{
    public class VipController : Controller
    {
        private readonly HistoryMadeSimpleContext _context;
        private readonly ILogger<LearnController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public VipController(HistoryMadeSimpleContext context, ILogger<LearnController> logger, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Detail()
        {
            return View();
        }
    }
}
