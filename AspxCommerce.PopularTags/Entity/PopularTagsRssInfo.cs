using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AspxCommerce.Core;

namespace AspxCommerce.PopularTags
{
    public class PopularTagsRssFeedInfo
    {
        public string TagName { get; set; }
        public string TagIDs { get; set; }
        public List<ItemCommonInfo> TagItemInfo { get; set; }

    }

    public class RssFeedNewTags
    {
        public string TagName { get; set; }
        public string TagStatus { get; set; }
        public string AddedOn { get; set; }
        public string TagIDs { get; set; }
        public List<ItemCommonInfo> TagItemInfo { get; set; }
    }
}
