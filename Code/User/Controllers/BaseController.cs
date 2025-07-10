using Microsoft.AspNetCore.Mvc;
using ViewModel.Base;

namespace User.Controllers
{
    public class BaseController : Controller
    {
        [HttpGet("404")]
        public IActionResult Error404(string action, string controller)
        {
            Error404ViewModel model = new Error404ViewModel
            {
                Action = action,
                Controller = controller,
                Message = "The requested resource was not found."
            };
            return View(model);
        }
        [HttpGet("500")]
        public IActionResult Error500(string returnAction, string returnController, string returnRegion)
        {
            Error500ViewModel model = new Error500ViewModel
            {
                Action = returnAction,
                Controller = returnController,
                region = returnRegion,
                Message = "An unexpected error occurred. Please try again later."
            };
            return View(model);
        }
    }
}
