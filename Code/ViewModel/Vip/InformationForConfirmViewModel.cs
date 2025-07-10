using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Vip
{
    public class InformationForConfirmViewModel
    {
        
        public string Name { get; set; } = string.Empty; 
        public string Email { get; set; } = string.Empty; 
        public string PhoneNumber { get; set; } = string.Empty; 
        public string Content { get; set; } = string.Empty; 
        public int PricePay { get; set; }
    }
}
