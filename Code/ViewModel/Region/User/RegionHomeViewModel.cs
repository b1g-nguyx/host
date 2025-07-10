using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Base;

namespace ViewModel.Region.User
{
    public class RegionHomeViewModel
    {
        public RegionViewModel regions { get; set; } = new RegionViewModel();
        public LeftSlideViewModel leftSlide { get; set; } = new LeftSlideViewModel();  
    }
}
