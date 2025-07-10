using DBAcess;
using DBAcess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using ViewModel.Base;
using ViewModel.Course.User;
using ViewModel.FlashCard.User;
using ViewModel.Learn.User;
using ViewModel.LearningMethods.User;
using ViewModel.Lesson.User;
using ViewModel.Quitz.User;
using ViewModel.Region.User;
using ViewModel.UserAnswers.User;
using ViewModel.Video.User;

namespace User.Controllers
{
        [Authorize]
    public class LearnController : Controller
    {
        private readonly HistoryMadeSimpleContext _context;
        private readonly ILogger<LearnController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public LearnController(HistoryMadeSimpleContext context,ILogger<LearnController> logger, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;

        }
        [HttpGet("[Controller]/{id}")]
        public async Task<IActionResult> Index(int id)
        {
            try
            {
              
                var lesson = await _context.Lessons.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (lesson == null)
                {
                    return RedirectToAction("Error500", "Base", new { action = "Index", controller = "Learn"});
                }

                var course = await _context.Courses.Where(x => x.Id == lesson.CourseId).FirstOrDefaultAsync();
                if(course == null)
                {
                    return RedirectToAction("Error500", "Base", new { action = "Index", controller = "Learn"});
                }
                var learningMethods = await _context.LearningMethods
                                            .Where(l => _context.LessonMethods
                                            .Any(lm => lm.LessonId == lesson.Id ))
                                            .AsNoTracking()
                                            .Distinct()
                                            .ToListAsync();

                if (learningMethods.Count == 0)
                {
                    return RedirectToAction("Error500", "Base", new { action = "Index", controller = "Learn"});
                }

                var returnRegion = await _context.Regions.Where(r => r.Id == lesson.RegionID).FirstOrDefaultAsync();
              
                var returnCourse = new CourseViewModel
                {
                    Id = course.Id,
                    Description = course.Description,
                    CourseName = course.CourseName
                };

                var returnLesson = new LessonViewModel
                {
                    Id = lesson.Id,
                    RegionID = lesson.RegionID,
                    Title = lesson.Title,
                    Content = lesson.Content,
                    Source = lesson.Source,
                    Authors = lesson.Authors,
                    Thumbnail = lesson.Thumbnail, 
                    CourseId = lesson.CourseId
                };

                var model = new LearnDetailViewModel
                {
                    Learn = new LearnViewModel
                    {
                        Lesson = returnLesson,
                        Course = returnCourse,
                    },
                    LeftSlide = new LeftSlideViewModel
                    {
                        sidebar = "learn",
                    },
                    LearnMethod = learningMethods.Select(lm => new LearningMethodViewModel
                    {
                        Id = lm.Id,
                        MethodName = lm.MethodName,
                        Description = lm.Description,
                        ImageBase64 = lm.LinkImage
                    }).ToList()
                };

                return View(model);
            }
            catch(Exception ex)
            {
                    return RedirectToAction("Error500", "Base", new { action = "Index", controller = "Learn"});
            }
        }
        [HttpGet("[Controller]/Flashcard/{id}")]
        public async Task<IActionResult> Flashcard(int id, int lessonId)
        {
            try
            {
                var lesson = await _context.Lessons.Where(l => l.Id == lessonId).AsNoTracking().FirstOrDefaultAsync();
                var lessonMethodIds = await _context.LessonMethods
                     .Where(lm => lm.LessonId == lessonId)
                     .Select(lm => lm.Id)
                     .ToListAsync();
                if(lessonMethodIds.Count == 0)
                {
                    return RedirectToAction("Error500", "Base", new { action = "Index", controller = "Learn"});
                }

                var flashCards = await _context.Flashcards
                    .Where(f => lessonMethodIds.Contains((int)f.LessonMethodId!))
                    .AsNoTracking()
                    .ToListAsync();
                if(flashCards.Count == 0)
                {
                    return RedirectToAction("Error500", "Base", new { action = "Index", controller = "Learn"});
                }


                var flashCardViewModels = flashCards.Select(f => new FlashCardViewModel
                {
                    Id = f.Id,
                    FrontContent = f.FrontContent,
                    BackContent = f.BackContent,
                    LessonMethodId = f.LessonMethodId
                }).ToList();

                var model = new FlashCardDeatilViewModel
                {
                    FlashCards = flashCardViewModels,
                    LeftSlide = new LeftSlideViewModel
                    {
                        sidebar = "learn",
                    },
                    Title = lesson!.Title,
                    LessonId = lessonId
                };

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Base", new { action = "Index", controller = "Learn"});
            }
        }
        [HttpGet("[Controller]/Quiz/{id}")]
        public async Task<IActionResult> Quiz( int id, int lessonId)
        {

            try
            {
                var quitz = new List<QuizQuestionViewModel>();
                var lesson = await _context.Lessons.FirstOrDefaultAsync(l => l.Id == lessonId);
                if (lesson == null)
                {
                    return RedirectToAction("Error500", "Base", new { action = "Index", controller = "Learn"});
                }

                   quitz = await _context.Questions
                           .Where(q => q.LessonID == lessonId)
                           .Select(q => new QuizQuestionViewModel
                           {
                               QuestionId = q.Id,
                               QuestionContent = q.Content,
                               Answers = _context.Answers
                                   .Where(a => a.QuestionId == q.Id)
                                   .Select(a => new QuizAnswerViewModel
                                   {
                                       AnswerId = a.Id,
                                       Content = a.Content
                                   }).ToList()
                           }).ToListAsync();

              

                var model = new QuitzViewModel
                {
                    Question = quitz,
                    LeftSlideViewModel = new LeftSlideViewModel
                    {
                        sidebar = "learn",
                    },
                    LessonId = lessonId,
                    Title = lesson.Title
                };
               
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error500", "Base", new { action = "Index", controller = "Learn" });
            }
        }
        [HttpGet("[Controller]/Video/{id}")]
        public async Task<IActionResult> VideoLearning(int id, int lessonId)
        {

            try
            {
               
                var lessonMethodIds = await _context.LessonMethods
                    .Where(lm => lm.LessonId == lessonId)
                    .Select(lm => lm.Id)
                    .ToListAsync();
                if (lessonMethodIds.Count == 0)
                {
                    return RedirectToAction("Error500", "Base", new { action = "Index", controller = "Learn"});
                }

                var video = await _context.Videos
                    .Where(f => lessonMethodIds.Contains((int)f.LessonMethodId!))
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
              
                var videoViewModels =  new VideoViewModel
                {
                    Id = video!.Id,
                    Title = video.Title,
                    Description = video.Description,
                    Url = video.Url,
                    Duration = video.Duration,
                    LessonMethodId = video.LessonMethodId
                };


                var model = new VideoDetailViewModel
                {
                    Video = videoViewModels,
                    LeftSlide = new LeftSlideViewModel
                    {
                        sidebar = "learn",
                    },
                    LessonId = lessonId
                };
                return View(model);
            }catch(Exception ex)
            {
                return RedirectToAction("Error500", "Base", new { action = "Index", controller = "Learn" });
            }


        }

        [HttpPost("[controller]/QuitzCheck")]
        public async Task<IActionResult> QuitzCheck(SubmitQuitzViewModel model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                if (user == null) return RedirectToAction("Login", "Account", new { action = "Index", controller = "Learn" });

                var userProfile = await _context.Users!.FirstOrDefaultAsync(u => u.UserId == user.Id);
                if (userProfile == null) return RedirectToAction("Login", "Account", new { action = "Index", controller = "Learn" });


                int correctCount = 0;
                List<UserAnswersViewModel> userAnswers = new List<UserAnswersViewModel>();
                foreach (var userAnswer in model.UserAnswers)
                {
                    bool isCorrect = false;
                    var correctAnswer = _context.Answers
                        .FirstOrDefault(a => a.QuestionId == userAnswer.QuestionId && a.IsCorrect);

                    if (correctAnswer != null && correctAnswer.Id == userAnswer.AnswerId)
                    {
                        isCorrect = true;
                        correctCount++;
                    }
                    var userAnswerEntity = new UserAnswersViewModel
                    {
                        UserId = userProfile.Id,
                        QuestionId = userAnswer.QuestionId,
                        Answer = "",
                        IsCorrect = isCorrect
                    };
                    userAnswers.Add(userAnswerEntity);
                }
                await _context.UserAnswers.AddRangeAsync(userAnswers.Select(ua => new DBAcess.Entities.UserAnswer
                {
                    UserID = ua.UserId,
                    QuestionID = ua.QuestionId,
                    AnswerText = ua.Answer,
                    IsCorrect = ua.IsCorrect
                }));
                await _context.SaveChangesAsync();
                int total = model.UserAnswers.Count;
                double score = (double)correctCount / total * 10;

                string html = $@"
                    <p><strong>Số câu đúng:</strong> {correctCount}/{total}</p>
                    <p><strong>Điểm:</strong> {score:0.0}</p>";

              var checkUpdate = UpdateProgress(model, score/total);
                if(!checkUpdate.Result)
                {
                    return RedirectToAction("Login", "Account", new { action = "Index", controller = "Learn" });
                }
                return Content(html, "text/html");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while checking the quiz answers.");
                return RedirectToAction("Error500", "Base", new { action = "Index", controller = "Learn" });
            }
        }

        public async Task<bool> UpdateProgress(SubmitQuitzViewModel model, double score)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null) return false;

            var userProfile = await _context.Users!.FirstOrDefaultAsync(u => u.UserId == user.Id);
            if (userProfile == null) return false;

            int lessonId = model.LessonId;

            var lessonProgress = await _context.LessonProgress
                .FirstOrDefaultAsync(lp => lp.UserID == userProfile.Id && lp.LessonID == lessonId);

            if (lessonProgress == null)
            {
                lessonProgress = new LessonProgress
                {
                    UserID = userProfile.Id,
                    LessonID = lessonId,
                    Status = "Completed",
                    ProgressPercentage = (int)(score * 100)
                };
                _context.LessonProgress.Add(lessonProgress);
            }
            else
            {
                lessonProgress.Status = "Completed";
                lessonProgress.ProgressPercentage = (int)(score * 100);
                _context.LessonProgress.Update(lessonProgress);
            }

            await _context.SaveChangesAsync();
            return true;
        }


    }
}
