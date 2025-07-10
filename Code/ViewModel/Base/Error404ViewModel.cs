using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Base
{
    public class Error404ViewModel
    {
        public string Action { get; set; } = string.Empty;
        public string Controller { get; set; } = string.Empty;
        public string Message { get; set; } = "The requested resource was not found.";
    }
}
