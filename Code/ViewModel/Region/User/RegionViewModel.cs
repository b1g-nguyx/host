using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.HistoricalSite.User;

namespace ViewModel.Region.User
{
    public class RegionViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Link { get; set; }
        public HistoricalSiteViewModel? HistoricalSite { get; set; }
    }
}
