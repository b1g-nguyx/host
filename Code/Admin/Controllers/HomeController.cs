using Admin.Models;
using DBAcess;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HistoryMadeSimpleContext _context;

        public HomeController(ILogger<HomeController> logger, HistoryMadeSimpleContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
      
    }
}
