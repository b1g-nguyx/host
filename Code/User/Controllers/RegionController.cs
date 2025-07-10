using DBAcess;
using DBAcess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViewModel.Course.User;
using ViewModel.HistoricalSite.User;
using ViewModel.Learn.User;
using ViewModel.Lesson.User;
using ViewModel.Region.User;

namespace User.Controllers
{
    public class RegionController : Controller
    {
        private readonly HistoryMadeSimpleContext _context;

        public RegionController(HistoryMadeSimpleContext context)
        {
            _context = context;
        }
      
        [HttpGet("Region/{region}")]
        public async Task<IActionResult> Index(string region)
        {
            RegionHomeViewModel item = new RegionHomeViewModel(); 
            string link = "/" + region;
            try
            {
                var regions = await _context.Regions.Where(p => p.Link == link).FirstOrDefaultAsync();
                if (regions == null) return RedirectToAction("Error404", "Base", new { action = "Index", controller = "Region" });
                var historicalSite = await _context.HistoricalSites
                    .Where(h => h.RegionID == regions.Id)
                    .FirstOrDefaultAsync();
              var  model = new RegionViewModel
                {
                    Id = regions.Id,    
                    Name = regions.RegionName,
                    Link = regions.Link,
                    HistoricalSite = new HistoricalSiteViewModel
                    {
                        Id = historicalSite!.Id,
                        RegionName = regions.RegionName,
                        SiteName = historicalSite.SiteName,
                        Description = historicalSite.Description,
                        RegionID = historicalSite.RegionID,
                        LinkImage360 = historicalSite.linkImage360
                    }
                };
                if(string.IsNullOrEmpty(historicalSite.Description) && string.IsNullOrEmpty(historicalSite.linkImage360)) return RedirectToAction("Error404", "Base", new { action = "Index", controller = "Region" });

                 item = new RegionHomeViewModel
                {
                    regions = model,
                    leftSlide = new ViewModel.Base.LeftSlideViewModel
                    {
                        region = region,
                        sidebar = "home",
                        navbar = "nav-historylocal-tab"
                    }
                };
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                return RedirectToAction("Error500", "Base", new { action = "Index", controller = "Region" });
            }
            return View(item);
        }
    }
}
