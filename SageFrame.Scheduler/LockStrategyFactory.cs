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

namespace SageFrame.Scheduler
{
    internal class LockStrategyFactory
    {
        public static ILockStrategy Create(LockingType strategy)
        {
            switch (strategy)
            {
                case LockingType.ReaderWriter:

                    return new LockStrategy();
                case LockingType.Exclusive:

                    return new ForcefulLockStrategy();
                default:

                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
