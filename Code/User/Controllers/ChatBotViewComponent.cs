using Microsoft.AspNetCore.Mvc;

namespace User.Controllers
{
    public class ChatBotViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("Default");
        }
    }
}
