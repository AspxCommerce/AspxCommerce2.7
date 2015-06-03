using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.BrandView
{
    public class BrandSettingInfo
    {
        public BrandSettingInfo()
        { 
        }
        public int BrandCount { get; set; }
        public string BrandAllPage { get; set; }
        public bool IsEnableBrandRss { get; set; }
        public int BrandRssCount { get; set; }
        public string BrandRssPage { get; set; }  
    }
}
