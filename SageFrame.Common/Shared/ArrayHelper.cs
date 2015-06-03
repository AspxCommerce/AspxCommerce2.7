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

namespace SageFrame.Common.Shared
{
    /// <summary>
    /// Array helper.
    /// </summary>
    public class ArrayHelper
    {
        /// <summary>
        /// Return "true" if array's are equal when compare two array. 
        /// </summary>
        /// <typeparam name="T">Type of the object implementing.</typeparam>
        /// <param name="a1">First array.</param>
        /// <param name="a2">Second array.</param>
        /// <returns>Return "true" if array's are equal.</returns>
        public static bool ArraysEqual<T>(T[] a1, T[] a2)
        {
            Array.Sort(a1);
            Array.Sort(a2);
            if (ReferenceEquals(a1, a2))
                return true;

            if (a1 == null || a2 == null)
                return false;

            if (a1.Length != a2.Length)
                return false;

            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            for (int i = 0; i < a1.Length; i++)
            {
                if (!comparer.Equals(a1[i], a2[i])) return false;
            }
            return true;
        }
    }
}
