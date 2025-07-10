using DBAcess;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly HistoryMadeSimpleContext _context;
        public UserController(HistoryMadeSimpleContext context, ILogger<UserController> logger)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
