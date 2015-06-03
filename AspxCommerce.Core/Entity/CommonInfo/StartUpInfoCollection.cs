using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class StartUpInfoCollection
    {
        public StartUpInfoCollection()
        {

        }

        public bool IsStoreAccess { get; set; }
        public bool IsStoreClosed { get; set; }
        public bool IsKPIInstalled { get; set; }
        public bool IsABTestInstalled { get; set; }
    }
}
