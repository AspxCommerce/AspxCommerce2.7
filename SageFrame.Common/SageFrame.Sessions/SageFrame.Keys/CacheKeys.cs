#region "Copyright"

/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/

#endregion

#region "References"

using System;
using System.Collections.Generic;
using System.Text; 

#endregion

namespace SageFrame.Common
{
    /// <summary>
    /// Application cache keys.
    /// </summary>
    [Serializable]
    public static partial class CacheKeys
    {
        public static string SageGoogleAnalytics = "SageGoogleAnalytics";
        public static string Portals = "Portals";
        public static string SageFrameCss = "SageFrameCss";
        public static string SageFrameJs = "SageFrameJs";
        public static string SageSetting = "SageSetting";
        public static string StartupSageSetting = "StartupSageSetting";
        public static string SetLayout = "SetLayout";
    }
}
