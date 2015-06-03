using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class BreadCrumInfo
    {
        public int PageID { get; set; }
        public string PageName { get; set; }
        public string TabPath { get; set; }
        public string SEOName { get; set; }
        public string LocalPage { get; set; }

        public BreadCrumInfo() { }

    }
}
