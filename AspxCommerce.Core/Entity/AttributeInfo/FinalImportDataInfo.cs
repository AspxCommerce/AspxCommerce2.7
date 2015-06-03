using System;
using System.Runtime.Serialization;

namespace AspxCommerce.Core
{
    public class FinalImportDataInfo
    {
        public int AttributeID { get; set; }
        public int InputTypeID { get; set; }
        public int ValidationTypeID { get; set; }
        public string Header { get; set; }
    }

    public class ItemsValues
    {
        public int AttributeId { get; set; }
        public int InputTypeID { get; set; }
        public int ValidationTypeID { get; set; }
        public object AttributeValue { get; set; }
    }

    public class CostVariantsOptions
    {
        public string ColumnName { get; set; }
        public string ColumnValue { get; set; }
    }
}