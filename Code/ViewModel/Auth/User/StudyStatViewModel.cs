using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Auth.User
{
    public class StudyStatViewModel
    {
        public string Date { get; set; } = null!; // yyyy-MM-dd
        public double TotalMinutes { get; set; }
    }
}
