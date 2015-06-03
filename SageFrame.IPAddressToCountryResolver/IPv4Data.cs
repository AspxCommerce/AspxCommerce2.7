#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Runtime.CompilerServices;
#endregion

namespace SageFrame.Framework
{
    /// <summary>
    /// 
    /// </summary>
    internal class IPv4Data
    {
        /// <summary>
        /// Get or set asigned value.
        /// </summary>
        public long Assigned
        {

            get;
            set;
        }
        /// <summary>
        /// Get or set country name.
        /// </summary>
        public string Country
        {

            get;
            set;
        }
        /// <summary>
        /// Get or set IP from.
        /// </summary>
        public long IpFrom
        {
            get;
            set;
        }
        /// <summary>
        /// Get or set IP to.
        /// </summary>
        public long IpTo
        {
            get;
            set;
        }
        /// <summary>
        /// Get or set ISO 3166 three letter code(County name code eg. HKG -- Hong Kong, NPL -- Nepal)
        /// </summary>
        public string Iso3166ThreeLetterCode
        {
            get;
            set;
        }
        /// <summary>
        /// Get or set ISO 3166 two letter code(County name code eg. HK -- Hong Kong, NP -- Nepal)
        /// </summary>
        public string Iso3166TwoLetterCode
        {
            get;
            set;
        }
        /// <summary>
        /// Get or set Registry value.
        /// </summary>
        public string Registry
        {
            get;
            set;
        }
    }
}

