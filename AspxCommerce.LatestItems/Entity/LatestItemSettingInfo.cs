using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.LatestItems
{
    public class LatestItemSettingInfo
    {
        public LatestItemSettingInfo()
        {
        }
        public bool IsEnableLatestItem { get; set; }
        public int LatestItemCount { get; set; }
        public int LatestItemInARow { get; set; }
        public bool IsEnableLatestRss { get; set; }
        public int LatestItemRssCount { get; set; }
        public string LatestItemRssPage { get; set; }
    }
}
