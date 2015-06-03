using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.YouMayAlsoLike
{
    public class YouMayAlsoLikeSettingInfo
    {
        public YouMayAlsoLikeSettingInfo()
        { 
        }
        public bool IsEnableYouMayAlsoLike { get; set; }
        public int YouMayAlsoLikeCount { get; set; }
        public int YouMayAlsoLikeInARow { get; set; }          
    }
}
