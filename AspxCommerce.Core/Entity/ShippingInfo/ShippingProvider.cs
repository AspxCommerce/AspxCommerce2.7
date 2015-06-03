using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{

    public class ShippingProvider
    {
        public int ShippingProviderID { get; set; }
        public string ShippingProviderName { get; set; }
        public string ShippingProviderServiceCode { get; set; }
        public string ShippingProviderAliasHelp { get; set; }
        public string AssemblyName { get; set; }
        public string ShippingProviderNamespace { get; set; }
        public string ShippingProviderClass { get; set; }
        public string LabelControlPath { get; set; }
        public string TrackControlPath { get; set; }
        public string SettingControlTempPath { get; set; }
        public string SettingControlPath { get; set; }
        public string ModuleFolder { get; set; }
        public string TempFolderPath { get; set; }
        public string TempFileName { get; set; }
        public string ExtractedPath { get; set; }
        public string ManifestFile { get; set; }
        public ArrayList DllFiles { get; set; }
        public decimal MinWeight { get; set; }
        public decimal MaxWeight { get; set; }
        public string WeightUnit { get; set; }
        public bool IsUnique { get; set; }
        public bool IsActive { get; set; }
        public string InstallScript { get; set; }
        public string UninstallScript { get; set; }
        public List<KeyValuePair<string, string>> Settings { get; set; }
        public List<DynamicMethod> DynamicMethods { get; set; }


    }

    public class ProviderShippingMethod
    {
        public string ShippingMethodName { get; set; }
        public string ShippingMethodCode { get; set; }
    }

   

   

}
