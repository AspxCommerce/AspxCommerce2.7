using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.CompareItem
{
    public class CompareItemsSettingInfo
    {
        public bool IsEnableCompareItem { get; set; }
        public int CompareItemCount { get; set; }
        public string CompareDetailsPage { get; set; }
    }

    public class CompareItemsSettingKeyPairInfo
    {
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }
    }
}
