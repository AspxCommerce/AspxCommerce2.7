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
using System.Threading;
#endregion

namespace SageFrame.Scheduler
{
    public class WatchLock: IDisposable,ILock
    {
        private ForcefulLockStrategy _lockStrategy;

        public WatchLock(ForcefulLockStrategy lockStrategy)
        {
            _lockStrategy = lockStrategy;
        }

        #region "IDisposable Support"

        // To detect redundant calls
        private bool _isDisposed;

       
        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _lockStrategy.Exit();
                    _lockStrategy = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                // TODO: set large fields to null.
            }
            _isDisposed = true;
        }

        #endregion
    }
}
