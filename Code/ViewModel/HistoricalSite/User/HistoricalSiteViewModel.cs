using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.HistoricalSite.User
{
    public class HistoricalSiteViewModel
    {
        public int Id { get; set; }
        public int RegionID { get; set; }
        public string? SiteName { get; set; }
        public string? Description { get; set; }
        public string? LinkImage360 { get; set; } // Changed to PascalCase for consistency
        public string? RegionName { get; set; } // Added to include region name in the view model
    }
}
