using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class ReviewStatusInfo
    {
        public ReviewStatusInfo()
        {

        }

        public bool IsReviewByIPExist { get; set; }
        public bool IsReviewByUserExist { get; set; }
    }
}
