using Azure;
using DBAcess;
using DBAcess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ViewModel.Course.User;
using ViewModel.Lesson.User;

namespace User.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private readonly HistoryMadeSimpleContext _context;
        private readonly ILogger<LearnController> _logger;

        public CourseController(HistoryMadeSimpleContext context, ILogger<LearnController> logger)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet("[controller]/Detail/{id}")]
        
        public async Task<IActionResult> Detail(string region,int id)
        {
            
            try
            {
                var lessons = await _context.Lessons.Where(l => l.CourseId == id).AsNoTracking().ToListAsync();

                if(lessons == null || lessons.Count == 0)
                {
                    return RedirectToAction("Error500", "Base", new { returnAction = "Detail", returnController = "Course" });
                }

                var course = await _context.Courses.Where(c => c.Id == id).AsNoTracking().FirstOrDefaultAsync();
                if(course == null)
                {
                    return RedirectToAction("Error500", "Base", new { returnAction = "Detail", returnController = "Course" });
                }

                var itemCourse = new CourseViewModel
                {
                    Id = course.Id,
                    CourseName = course.CourseName,
                    Description = course.Description,
                    ImageBase64 = course.ImageBase64
                };

                var itemLessons = lessons.Select(l => new LessonViewModel
                {
                    Id = l.Id,
                    RegionID = l.RegionID,
                    Title = l.Title,
                    Content = l.Content,
                    CourseId = l.CourseId,
                    Thumbnail = l.Thumbnail
                }).ToList();

                CourseDetailViewModel model = new CourseDetailViewModel
                {
                    course = itemCourse,
                    LeftSlide = new ViewModel.Base.LeftSlideViewModel
                    {
                        sidebar = "learn",
                        navbar = "nav-course-tab",
                        region = region
                    },
                  Lesson = itemLessons
                };

                return View(model);
            }catch(Exception ex)
            {
                return RedirectToAction("Error500", "Base", new { returnAction = "Detail", returnController = "Course" });
            }
        }
        
        
        [HttpGet("[controller]/AllCourse")]
        public async Task<IActionResult> AllCourse(string region,int page = 1)
        {
            try
            {
                int pageSize = 20;
                if (page < 1) page = 1;

                var query = _context.Courses;

                var totalItems = await query.CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

                if (page > totalPages && page != 1) page = totalPages;

                var courses = await query
                    .OrderBy(c => c.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                if (courses.Count == 0)
                {
                    return RedirectToAction("Error500", "Base", new { returnAction = "Index", returnController = "Course"});
                }

                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = totalPages;

                var items = courses.Select(course => new CourseViewModel
                {
                    Id = course.Id,
                    CourseName = course.CourseName,
                    Description = course.Description,
                    ImageBase64 = course.ImageBase64
                }).ToList();

                CourseHomeViewModel courseHome = new CourseHomeViewModel
                {
                    courses = items,
                    leftSlide = new ViewModel.Base.LeftSlideViewModel
                    {
                        sidebar = "learn",
                        navbar = "nav-course-tab",
                    }
                };
                return View(courseHome);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error500", "Base", new { returnAction = "Index", returnController = "Course" });

            }
        }


        [HttpGet("[controller]/{region}")]
        public async Task<IActionResult> Index(string region, int page = 1)
        {
            try
            {
                int pageSize = 5;
                if (page < 1) page = 1;

                string link = "/" + region;
                var query = _context.Courses
                            .Where(c => _context.Lessons
                                    .Any(l => l.CourseId == c.Id &&
                                         _context.Regions
                                                .Any(r => r.Id == l.RegionID && r.Link == link
                                                )));


                var totalItems = await query.CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

                if (page > totalPages && page != 1) page = totalPages;

                var courses = await query
                    .OrderBy(c => c.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                    
                if(courses.Count == 0)
                {
                return RedirectToAction("Error500", "Base", new { returnAction = "Index", returnController = "Course", returnRegion = region });
                }

                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = totalPages;

                var items = courses.Select(course => new CourseViewModel
                {
                    Id = course.Id,
                    CourseName = course.CourseName,
                    Description = course.Description
                }).ToList();

                CourseHomeViewModel courseHome = new CourseHomeViewModel
                {
                    courses = items,
                    leftSlide = new ViewModel.Base.LeftSlideViewModel
                    {
                        sidebar = "learn",
                        navbar = "nav-course-tab",
                        region = region,
                    }
                };
            return View(courseHome);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error500", "Base", new { returnAction = "Index", returnController = "Course", returnRegion = region });

            }
        }


      
    }
}
