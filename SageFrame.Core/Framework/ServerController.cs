#region "Copyright"

/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/

#endregion

#region "References"

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 

#endregion

namespace SageFrame.Framework
{
    /// <summary>
    /// Server Controller class
    /// </summary>
    public class ServerController
    {
        public ServerController()
        {

        }

        /// <summary>
        /// Returns true if the application is web farm
        /// </summary>
        public bool IsWebFarm
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Retruns true if the application is Sagever
        /// </summary>
        public bool IsSagever
        {
            get
            {
                return false;
            }
        }

    }
}
