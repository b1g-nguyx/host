using DBAcess;
using DBAcess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using ViewModel.Auth.User;
using System.Globalization;

namespace User.APITest
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly HistoryMadeSimpleContext _context;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        public ValuesController(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    HistoryMadeSimpleContext context,
    IEmailSender emailSender,
    IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _emailSender = emailSender;
            _configuration = configuration;
        }


        [HttpPost("ImportCsv")]
        public async Task<IActionResult> ImportCsv(IFormFile file)
        {
            if (file == null || file.Length <= 0)
                return BadRequest("File is empty");

            if (!Path.GetExtension(file.FileName).Equals(".csv", StringComparison.OrdinalIgnoreCase))
                return BadRequest("Chỉ hỗ trợ file .csv");

            var students = new List<(int LineNumber, StudentInfo Student)>();
            var errorLogs = new List<string>();
            var successLogs = new List<string>();
            int successCount = 0;

            using (var stream = new StreamReader(file.OpenReadStream()))
            {
                string? line;
                bool isHeader = true;
                int lineNumber = 0;

                while ((line = await stream.ReadLineAsync()) != null)
                {
                    lineNumber++;

                    if (isHeader)
                    {
                        isHeader = false;
                        continue;
                    }

                    try
                    {
                        var columns = line.Split(',');

                        if (columns.Length < 11)
                        {
                            errorLogs.Add($"❌ Dòng {lineNumber}: Thiếu cột");
                            continue;
                        }

                        var student = new StudentInfo
                        {
                            UserId = Guid.Parse(columns[0]),
                            UserName = columns[1],
                            Email = columns[2],
                            FullName = columns[3],
                            SchoolName = columns[4],
                            UserType = columns[5],
                            CreatedAt = DateTime.Parse(columns[6]),
                            LoginTime = DateTime.Parse(columns[7]),
                            LogoutTime = DateTime.Parse(columns[8]),
                            IPAddress = columns[9],
                            UserAgent = columns[10]
                        };

                        students.Add((lineNumber, student));
                    }
                    catch (Exception ex)
                    {
                        errorLogs.Add($"❌ Dòng {lineNumber}: Lỗi đọc dữ liệu - {ex.Message}");
                        continue;
                    }
                }
            }

            foreach (var (lineNumber, model) in students)
            {
                try
                {
                    var existingUser = await _userManager.FindByEmailAsync(model.Email);
                    if (existingUser != null)
                    {
                        errorLogs.Add($"⚠️ Dòng {lineNumber}: Email đã tồn tại - {model.Email}");
                        continue;
                    }

                    var user = new ApplicationUser
                    {
                        UserName = model.Email,
                        Email = model.Email,
                        EmailConfirmed = true,
                        CreatedAt = model.CreatedAt,
                    };

                    var password = "Password@123";
                    var result = await _userManager.CreateAsync(user, password);

                    if (!result.Succeeded)
                    {
                        errorLogs.Add($"❌ Dòng {lineNumber}: Lỗi tạo user - {model.Email}, Errors: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                        continue;
                    }

                    var userProfile = new DBAcess.Entities.User
                    {
                        UserId = user.Id,
                        FullName = model.FullName,
                        SchoolName = model.SchoolName,
                    };

                    _context.Users.Add(userProfile);

                    var userSession = new UserSession
                    {
                        UserId = user.Id,
                        LoginTime = model.LoginTime,
                        LogoutTime = model.LogoutTime,
                        IPAddress = model.IPAddress,
                        UserAgent = model.UserAgent
                    };

                    _context.UserSessions.Add(userSession);

                    successCount++;
                    successLogs.Add($"✅ Dòng {lineNumber}: {model.Email}");
                }
                catch (Exception ex)
                {
                    errorLogs.Add($"❌ Dòng {lineNumber}: Lỗi khi lưu DB - {model.Email} - {ex.Message}");
                    continue;
                }
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = $"Import CSV hoàn tất",
                SuccessCount = successCount,
                SuccessLines = successLogs,
                ErrorCount = errorLogs.Count,
                Errors = errorLogs
            });
        }


        public class StudentInfo
        {
            public Guid UserId { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string FullName { get; set; }
            public string SchoolName { get; set; }
            public string UserType { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime LoginTime { get; set; }
            public DateTime LogoutTime { get; set; }
            public string IPAddress { get; set; }
            public string UserAgent { get; set; }
        }
    }
}
