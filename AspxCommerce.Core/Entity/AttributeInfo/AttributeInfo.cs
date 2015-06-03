using System;
using System.Runtime.Serialization;

namespace AspxCommerce.Core
{
    public class AttributeInfo
    {
        public int AttributeID { get; set; }
        public string AttributeName { get; set; }
        public int InputTypeID { get; set; }
        public int ValidationTypeID { get; set; }
    }
}
