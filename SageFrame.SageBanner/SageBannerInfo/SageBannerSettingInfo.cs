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

namespace SageFrame.SageBannner.SettingInfo
{
    /// <summary>
    ///  This class holds the properties for SageBannerSettingInfo.
    /// </summary>
    public class SageBannerSettingInfo
    {  
        /// <summary>
        /// Gets or sets auto direction.
        /// </summary>
        public string Auto_Direction { get; set; }
        /// <summary>
        /// Gets or sets boolean value to check for auto hover.
        /// </summary>
        public bool Auto_Hover { get; set; }
        /// <summary>
        /// Gets or sets boolean value to check for auto slide.
        /// </summary>
        public bool Auto_Slide { get; set; }
        /// <summary>
        /// Gets or sets banner to use.
        /// </summary>
        public string BannerToUse { get; set; }
        /// <summary>
        /// Gets or sets boolean value to check for caption.
        /// </summary>
        public bool Caption { get; set; }
        public int DisplaySlideQty { get; set; }
        public string Easing { get; set; }
        /// <summary>
        /// Gets or sets  infiniteloop
        /// </summary>
        public bool InfiniteLoop { get; set; }
        /// <summary>
        /// Gets or sets list of SageBannerSettingInfo objects
        /// </summary>
        public List<SageBannerSettingInfo> ListSage { get; set; }
        /// <summary>
        /// Gets or sets move slide.
        /// </summary>
        public int MoveSlideQty { get; set; }
        //Gets or sets boolean value to check for navigation image pager.
        public bool NavigationImagePager { get; set; }
        /// <summary>
        /// Gets or sets boolean value to check for numeric pager..
        /// </summary>
        public bool NumericPager { get; set; }
        /// <summary>
        /// Gets or sets pause time.
        /// </summary>
        public int Pause_Time { get; set; }
        /// <summary>
        /// Gets or sets boolean value to check for random start.
        /// </summary>
        public bool RandomStart { get; set; }
        /// <summary>
        /// Gets or sets speed.
        /// </summary>
        public int Speed { get; set; }
        /// <summary>
        /// Gets or sets starting slide.
        /// </summary>
        public int Starting_Slide { get; set; }
        /// <summary>
        /// Gets or sets transition mode.
        /// </summary>
        public string TransitionMode { get; set; }
        /// <summary>
        /// Gets or sets boolean value to check for enable control.
        /// </summary>
        public bool EnableControl { get; set; }
        /// <summary>
        /// Initializes a new instance of the SageBannerSettingInfo class.
        /// </summary>
        public SageBannerSettingInfo() { }

    }
}

