using DBAcess;
using DBAcess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;
using System.Web;
using ViewModel.Auth.User;

namespace User.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly HistoryMadeSimpleContext _context;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        public AccountController(
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

        [HttpGet]
        public IActionResult Register() => View();


        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Kiểm tra xem người dùng đã tồn tại chưa
            var existingUser = await _userManager.FindByEmailAsync(model.Email);

            if (existingUser != null)
            {
                if (!await _userManager.IsEmailConfirmedAsync(existingUser))
                {
                    // Gửi lại email xác nhận
                    var resendToken = await _userManager.GenerateEmailConfirmationTokenAsync(existingUser);
                    var encodedResendToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(resendToken));

                    var resendLink = Url.Action("ConfirmEmail", "Account", new
                    {
                        userId = existingUser.Id,
                        token = encodedResendToken
                    }, Request.Scheme);

                    await _emailSender.SendEmailAsync(model.Email, "Xác nhận lại email",
                        $@"
                <div style='font-family: Arial, sans-serif; max-width: 600px; margin: auto; padding: 30px; background-color: #ffffff; border-radius: 12px; border: 1px solid #e0e0e0; box-shadow: 0 2px 8px rgba(0,0,0,0.05);'>
                    <div style='text-align: center;'>
                        <h2 style='color: #28a745;'>Xác nhận tài khoản của bạn</h2>
                    </div>
                    <p style='color: #333333; font-size: 16px;'>Chào bạn,</p>
                    <p style='color: #333333; font-size: 16px; line-height: 1.6;'>
                        Email này đã được đăng ký nhưng chưa được xác thực. Vui lòng xác nhận email bằng cách nhấn vào nút bên dưới:
                    </p>
                    <div style='text-align: center; margin: 40px 0;'>
                        <a href='{resendLink}' style='display: inline-block; background-color: #28a745; color: #ffffff; padding: 14px 28px; border-radius: 6px; text-decoration: none; font-size: 16px; font-weight: bold; box-shadow: 0 4px 6px rgba(0,0,0,0.1); transition: background-color 0.3s ease;'>
                            Xác nhận email
                        </a>
                    </div>
                    <p style='color: #666666; font-size: 14px;'>Nếu bạn không yêu cầu đăng ký, bạn có thể bỏ qua email này.</p>
                    <hr style='margin: 30px 0; border: none; border-top: 1px solid #e0e0e0;'>
                    <p style='font-size: 12px; color: #999999; text-align: center;'>Email này được gửi tự động. Vui lòng không trả lời.</p>
                </div>
                ");

                    TempData["Message"] = "Email đã tồn tại nhưng chưa xác thực. Đã gửi lại liên kết xác nhận.";
                    return RedirectToAction("Login");
                }

                ModelState.AddModelError("", "Email đã được đăng ký.");
                return View(model);
            }

            // Nếu chưa tồn tại → tạo mới
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Tạo User Profile
                var userProfile = new DBAcess.Entities.User
                {
                    UserId = user.Id,
                    FullName = model.FullName,
                    SchoolName = model.SchoolName,
                    UserType = "Normal"
                };
                _context.Users.Add(userProfile);
                await _context.SaveChangesAsync();

                // Gửi mail xác nhận
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

                var confirmationLink = Url.Action("ConfirmEmail", "Account", new
                {
                    userId = user.Id,
                    token = encodedToken
                }, Request.Scheme);

                await _emailSender.SendEmailAsync(model.Email, "Xác nhận email",
                     $@"
            <div style='font-family: Arial, sans-serif; max-width: 600px; margin: auto; padding: 30px; background-color: #ffffff; border-radius: 12px; border: 1px solid #e0e0e0; box-shadow: 0 2px 8px rgba(0,0,0,0.05);'>
                <div style='text-align: center;'>
                    <h2 style='color: #28a745;'>Xác nhận tài khoản của bạn</h2>
                </div>
                <p style='color: #333333; font-size: 16px;'>Chào bạn,</p>
                <p style='color: #333333; font-size: 16px; line-height: 1.6;'>
                    Cảm ơn bạn đã đăng ký tài khoản tại hệ thống của chúng tôi. Vui lòng xác nhận email bằng cách nhấn vào nút bên dưới:
                </p>
                <div style='text-align: center; margin: 40px 0;'>
                    <a href='{confirmationLink}' style='display: inline-block; background-color: #28a745; color: #ffffff; padding: 14px 28px; border-radius: 6px; text-decoration: none; font-size: 16px; font-weight: bold; box-shadow: 0 4px 6px rgba(0,0,0,0.1); transition: background-color 0.3s ease;'>
                        Xác nhận email
                    </a>
                </div>
                <p style='color: #666666; font-size: 14px;'>Nếu bạn không yêu cầu đăng ký, bạn có thể bỏ qua email này.</p>
                <hr style='margin: 30px 0; border: none; border-top: 1px solid #e0e0e0;'>
                <p style='font-size: 12px; color: #999999; text-align: center;'>Email này được gửi tự động. Vui lòng không trả lời.</p>
            </div>
            ");

                return RedirectToAction("EmailVerificationNotice");
            }

            // Nếu tạo user thất bại
            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }



        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
                return RedirectToAction("Index", "Home");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound("Người dùng không tồn tại.");

            try
            {
                var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
                var result = await _userManager.ConfirmEmailAsync(user, decodedToken);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (FormatException)
            {
                // Log lỗi token không hợp lệ
                return RedirectToAction("InvalidToken", "Account");
            }

            return RedirectToAction("Error500", "Base", new { action = "Index", controller = "Learn" });
        }


        [HttpGet]
        public IActionResult EmailVerificationNotice() => View();

        [HttpGet]
        public IActionResult Login() => View();


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Tài khoản không tồn tại.");
                return View(model);
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError(string.Empty, "Email của bạn chưa được xác nhận. Vui lòng kiểm tra hộp thư.");
                return View(model);
            }

            if (!await _userManager.CheckPasswordAsync(user, model.Password))
            {
                ModelState.AddModelError(string.Empty, "Sai mật khẩu.");
                return View(model);
            }

            var userProfile = await _context.Users!.FirstOrDefaultAsync(u => u.UserId == user.Id);
            if (userProfile != null)
            {
                HttpContext.Session.SetString("FullName", userProfile.FullName!);
            }

            var claims = new List<Claim>
    {
        new Claim("UserId", user.Id.ToString())
    };

            await _signInManager.SignInWithClaimsAsync(user, isPersistent: model.RememberMe, claims);

            await LogUserSessionAsync(user); // ghi log nếu cần

            return RedirectToLocal(returnUrl);
        }

        private IActionResult RedirectToLocal(string? returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }


        private async Task LogUserSessionAsync(ApplicationUser user)
        {
            var session = new UserSession
            {
                UserId = user.Id,
                LoginTime = DateTime.UtcNow,
                IPAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                UserAgent = Request.Headers["User-Agent"].ToString()
            };

            _context.UserSessions.Add(session);
            await _context.SaveChangesAsync();
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            var userId = _userManager.GetUserId(User);
            var session = await _context.UserSessions
                .Where(x => x.UserId == userId && x.LogoutTime == null)
                .OrderByDescending(x => x.LoginTime)
                .FirstOrDefaultAsync();

            if (session != null)
            {
                session.LogoutTime = DateTime.UtcNow;
                _context.Update(session);
                await _context.SaveChangesAsync();
            }

            await _signInManager.SignOutAsync();
            HttpContext.Session.Clear(); 
            return RedirectToAction("Index", "Home");
        }

        
    }
}
