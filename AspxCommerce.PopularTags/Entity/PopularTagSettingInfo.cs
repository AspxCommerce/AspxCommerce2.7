using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.PopularTags
{
    public class PopularTagsSettingInfo
    {
        public bool IsEnablePopularTag { get; set; }
        public int PopularTagCount { get; set; }
        public int TaggedItemInARow { get; set; }
        public bool IsEnablePopularTagRss { get; set; }
        public int PopularTagRssCount { get; set; }
        public string PopularTagsRssPageName { get; set; }
        public string ViewAllTagsPageName { get; set; }
        public string ViewTaggedItemPageName { get; set; }
    }

    public class PopularTagsSettingKeyPair
    {
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }
    }
}
