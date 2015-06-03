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
    class LockReaderWriter : ILock
    {
        private bool _disposed;
        private ReaderWriterLockSlim _lock;

        public LockReaderWriter(ReaderWriterLockSlim @lock)
        {
            _lock = @lock;
        }

        #region ISharedCollectionLock Members

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        #endregion

        private void EnsureNotDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("ReaderWriterSlimLock");
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    //free managed resources here
                }

                //free unmanaged resrources here
                if (_lock.IsReadLockHeld)
                {
                    _lock.ExitReadLock();
                }
                else if (_lock.IsWriteLockHeld)
                {
                    _lock.ExitWriteLock();
                }
                else if (_lock.IsUpgradeableReadLockHeld)
                {
                    _lock.ExitUpgradeableReadLock();
                }

                _lock = null;
                _disposed = true;
            }
        }

        ~LockReaderWriter()
        {
            Dispose(false);
        }
    }
}
