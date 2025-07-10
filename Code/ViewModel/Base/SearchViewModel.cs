using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Base
{
    public class SearchViewModel
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string Search { get; set; }
    }
}
