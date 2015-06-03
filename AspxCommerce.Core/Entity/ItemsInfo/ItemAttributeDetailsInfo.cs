using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AspxCommerce.Core
{
    [DataContract]
    public class ItemAttributeDetailsInfo
    {
        public int ItemID { get; set; }
        public int AttributeID { get; set; }
        public string AttributeName { get; set; }
        public int InputTypeID { get; set; }
        public int ValidationTypeID { get; set; }
        public int GroupID { get; set; }
        public bool IsIncludeInPriceRule { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsUnique { get; set; }
        public bool IsRequired { get; set; }
        public string NvarcharValue { get; set; }
        public string TextValue { get; set; }
        public bool BooleanValue { get; set; }
        public int IntValue { get; set; }
        public System.Nullable<DateTime> DateValue { get; set; }
        public System.Nullable<decimal> DecimalValue { get; set; }
        public string FileValue { get; set; }
        public string OptionValues { get; set; }
    }
}
