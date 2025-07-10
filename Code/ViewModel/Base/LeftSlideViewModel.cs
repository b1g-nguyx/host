using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Base
{
    public class LeftSlideViewModel
    {
        public string region { get; set; } = string.Empty;
        public string sidebar { get; set; } = string.Empty;
        public string navbar { get; set; } = string.Empty;
        public string tab { get; set; } = "nav-tab-tab";
        public string news { get; set; } = "nav-new-tab";
        public string video { get; set; } = "nav-video-tab";

        
    }
}
