using Azure;
using DBAcess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ViewModel.Base;
using ViewModel.Course.User;
using ViewModel.Video.User;

namespace User.Controllers
{
    public class VideoController : Controller
    {
        private readonly HistoryMadeSimpleContext _context;
        private readonly ILogger<LearnController> _logger;

        public VideoController(HistoryMadeSimpleContext context, ILogger<LearnController> logger)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<IActionResult> Index(string search, int page = 1)
        {
            try
            {
                int pageSize = 21;
                if (page < 1) page = 1;

                // Bắt đầu với truy vấn gốc
                var query = _context.Videos.AsQueryable().Where(x => x.LessonMethodId == 25);

                // Nếu có từ khóa tìm kiếm thì lọc theo tiêu đề
                if (!string.IsNullOrWhiteSpace(search))
                {
                    query = query.Where(v => v.Title.Contains(search));
                }

                // Đếm tổng số bản ghi sau khi lọc
                var totalItems = await query.CountAsync(x => x.LessonMethodId == 25);
                var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

                if (page > totalPages && page != 1) page = totalPages;

                // Phân trang
                var videos = await query
                    .OrderBy(c => c.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                // Trường hợp không có video nào
                if (videos.Count == 0)
                {
                    return RedirectToAction("Error500", "Base", new { returnAction = "Index", returnController = "Course" });
                }

                var searchView = new SearchViewModel
                {
                    Search = search,
                    CurrentPage = page,
                    TotalPages = totalPages,
                };
                Console.Write(totalItems);
                // Mapping sang ViewModel
                var items = videos.Select(video => new VideoViewModel
                {
                    Id = video.Id,
                    Title = video.Title,
                    Thumbnail = video.Thumbnail,
                }).ToList();

                var videoHome = new VideoAllViewModel
                {
                    Search = searchView,
                    Video = items,
                    LeftSlide = new ViewModel.Base.LeftSlideViewModel
                    {
                        sidebar = "video",
                    }
                };

                return View(videoHome);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error500", "Base", new { returnAction = "Index", returnController = "Course" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Detail (int id)
        {
            try
            {
                var mainVideo = await _context.Videos.Where(v => v.Id == id).Select( v => new VideoViewModel
                {
                    Id = v.Id,
                    Title = v.Title,
                    Thumbnail = v.Thumbnail,
                    Url = v.Url,
                    Description = v.Description,
                    LessonMethodId = v.LessonMethodId,
                    Duration = v.Duration
                }).FirstOrDefaultAsync();

                if (mainVideo == null) { 
                return RedirectToAction("Error500", "Base", new { returnAction = "Index", returnController = "Video" });
                }
                var query = _context.Videos.AsQueryable().Where(x => x.LessonMethodId == 25);

                var videos = await query
                    .OrderBy(c => c.Id)
                    .Skip((0) * 10)
                    .Take(10)
                    .ToListAsync();
                // Trường hợp không có video nào
                if (videos.Count == 0)
                {
                    return RedirectToAction("Error500", "Base", new { returnAction = "Index", returnController = "Course" });
                }

                var listVideo = videos.Select( x => new VideoViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Thumbnail = x.Thumbnail,
                    Url = x.Url,
                    Description = x.Description,
                    LessonMethodId = x.LessonMethodId,
                    Duration = x.Duration
                }).ToList();

                var model = new VideoDetailAllViewModel
                {
                    LeftSlide = new ViewModel.Base.LeftSlideViewModel
                    {
                        sidebar = "video",
                    },
                    MainVideo = mainVideo,
                    Video = listVideo
                };

                return View(model);
            }catch(Exception ex)
            {
                return RedirectToAction("Error500", "Base", new { returnAction = "Index", returnController = "Video" });

            }
        }
    }
}
