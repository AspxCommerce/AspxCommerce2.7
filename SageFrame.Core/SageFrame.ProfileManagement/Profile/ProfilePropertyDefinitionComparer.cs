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
using SageFrame.ProfileManagement;

#endregion


namespace SageFrame.Profile
{
    /// <summary>
    /// Profile comparer
    /// </summary>
    public class ProfilePropertyDefinitionComparer : System.Collections.IComparer
    {

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Compares two sp_ProfileListActiveResult objects
        /// </summary>
        /// <param name="x">A sp_ProfileListActiveResult object</param>
        /// <param name="y">A sp_ProfileListActiveResult object</param>
        /// <returns>An integer indicating whether x greater than y, x=y or x less than y</returns>
        /// <history>
        ///      [alok]    03/25/2010    created
        /// </history>
        ///  -----------------------------------------------------------------------------
        public int Compare(object x, object y)
        {
            return (Int32.Parse(((ProfileManagementInfo)(x)).DisplayOrder.ToString()).CompareTo(Int32.Parse(((ProfileManagementInfo)(y)).DisplayOrder.ToString())));
        }
    }
}
