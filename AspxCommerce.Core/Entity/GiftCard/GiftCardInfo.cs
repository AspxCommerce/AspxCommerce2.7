using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class GiftCardInfo
    {
        
        public int ItemId { get; set; }
        public int GiftCardCategoryId  { get; set; }
        public string GiftCardCategory  { get; set; }
        public string GraphicName  { get; set; }
        public string GraphicImage { get; set; }
        public int GiftCardGraphicId { get; set; }
     
        
    }

    public class GiftCardCategoryInfo
    {
        public int RowTotal { get; set; }
        public int GiftCardCategoryId { get; set; }
        public string GiftCardCategory { get; set; }
        public DateTime AddedOn { get; set; }
        public bool IsActive { get; set; }

    }
}

