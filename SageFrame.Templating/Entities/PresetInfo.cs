#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace SageFrame.Templating
{
    /// <summary>
    /// This class holds the properties of PresetInfo.
    /// </summary>
    public class PresetInfo
    {
        /// <summary>
        /// get or set preset name.
        /// </summary>
        public string PresetName { get; set; }
        /// <summary>
        /// Get or set active theme.
        /// </summary>
        public string ActiveTheme { get; set; }
        /// <summary>
        /// Get or set active layout.
        /// </summary>
        public string ActiveLayout { get; set; }
        /// <summary>
        /// Get or set active width.
        /// </summary>
        public string ActiveWidth { get; set; }
        /// <summary>
        /// Get or set true if css optimization enable.
        /// </summary>
        public bool IsCssOptimizationEnabled { get; set; }
        /// <summary>
        /// Get or set true if js optimization enable.
        /// </summary>
        public bool IsJsOptimizationEnabled { get; set; }
        /// <summary>
        /// Get or set pages.
        /// </summary>
        public string Pages { get; set; }
        /// <summary>
        /// Get or set list of pages.
        /// </summary>
        public List<string> LSTPages { get; set; }
        /// <summary>
        /// Get or set true for cpanel accessable.
        /// </summary>
        public bool CPanel { get; set; }
        /// <summary>
        /// Get or set true for handheld device.
        /// </summary>
        public bool HandHeld { get; set; }
        /// <summary>
        /// Get or set true for default layout.
        /// </summary>
        public bool IsDefault { get; set; }
        /// <summary>
        /// Get or set handheld layout name.
        /// </summary>
        public string HandHeldLayout { get; set; }
        /// <summary>
        /// Get or set list of layouts.
        /// </summary>
        public List<KeyValue> lstLayouts { get; set; }
        /// <summary>
        /// Get or set list of pages.
        /// </summary>
        public List<string> lstPages { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_PresetName">Preset name.</param>
        /// <returns></returns>
        public PresetInfo GetPresetObject(string _PresetName)
        {
            return new PresetInfo
            {
                PresetName=_PresetName
            };
        }
       
        /// <summary>
        /// obtain object of PresetInfo class.
        /// </summary>
        /// <param name="_PresetName">Preset name.</param>
        /// <param name="_Pages">Pages.</param>
        /// <returns>object of PresetInfo class.</returns>
        public static PresetInfo GetPresetPages(string _PresetName, string _Pages)
        {
           
            return new PresetInfo
            {
                 
                PresetName=_PresetName,
                Pages=_Pages
            };
        }
        /// <summary>
        /// Obtain list of preset stings.
        /// </summary>
        /// <returns>List of preset settings.</returns>
        public static List<string> GetPresetSettings()
        {
            List<string> presetCols = new List<string>();
            presetCols.Add("activelayout");
            presetCols.Add("activetheme");
            presetCols.Add("activewidth");
            presetCols.Add("cssopt");
            presetCols.Add("jsopt");
            presetCols.Add("cpanel");
            presetCols.Add("handheld");
            presetCols.Add("handheldlayout");
            return presetCols;
        }

    }
}
