using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Auth.User
{
    using System.ComponentModel.DataAnnotations;

    public class ProfileViewModel
    {
        public string UserId { get; set; }

        [Display(Name = "Họ tên")]
        [Required]
        public string FullName { get; set; }

        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }
        [Display(Name = "Trường học")]
        public string? SchoolName { get; set; }
        [Display(Name = "Email")]
        [Required, EmailAddress]
        public string Email { get; set; }
    }

}
