using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.AdvanceSearch
{
    public class AdvanceSearchSettingInfo
    {

        public bool IsEnableAdvanceSearch { get; set; }        
        public bool IsEnableBrandSearch { get; set; }
        public int NoOfItemsInARow { get; set; }
        public string AdvanceSearchPageName { get; set; }  
    }

    public class AdvanceSearchSettingKeyPairInfo
    {
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }
    }
}
