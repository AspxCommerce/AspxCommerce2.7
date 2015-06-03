using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.SpecialItems
{
    public class SpecialItemsSettingInfo
    {
        public bool IsEnableSpecialItems { get; set; }
        public int NoOfItemShown { get; set; }
        public int NoOfItemInRow { get; set; }        
        public bool IsEnableSpecialItemsRss { get; set; }
        public int SpecialItemsRssCount { get; set; }
        public string SpecialItemsRssPageName { get; set; }
        public string SpecialItemsDetailPageName { get; set; }
    }

    public class SpecialItemsSettingKeyPairInfo
    {
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }
    }
}
