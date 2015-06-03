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
    public class ForcefulLockStrategy: ILockStrategy
    {
        private readonly object _lock = new object();

        private bool _isDisposed;
        private Thread _lockedThread;

        #region ILockStrategy Members

        public ILock GetReadLock()
        {
            return GetLock(TimeSpan.FromMilliseconds(-1));
        }

        public ILock GetReadLock(TimeSpan timeout)
        {
            return GetLock(timeout);
        }

        public ILock GetWriteLock()
        {
            return GetLock(TimeSpan.FromMilliseconds(-1));
        }

        public ILock GetWriteLock(TimeSpan timeout)
        {
            return GetLock(timeout);
        }

        public bool ThreadCanRead
        {
            get
            {
                EnsureNotDisposed();
                return IsThreadLocked();
            }
        }

        public bool ThreadCanWrite
        {
            get
            {
                EnsureNotDisposed();
                return IsThreadLocked();
            }
        }

        public bool SupportsConcurrentReads
        {
            get
            {
                return false;
            }
        }

        public void Dispose()
        {
            _isDisposed = true;
            //todo remove disposable from interface?
        }

        #endregion

        private ILock GetLock(TimeSpan timeout)
        {
            EnsureNotDisposed();
            if (IsThreadLocked())
            {
                throw new LockRecursionException();
            }

            if (Monitor.TryEnter(_lock, timeout))
            {
                _lockedThread = Thread.CurrentThread;
                return new WatchLock(this);
            }
            else
            {
                throw new ApplicationException("ExclusiveLockStrategy.GetLock timed out");
            }
        }

        private ILock GetLock()
        {
            EnsureNotDisposed();
            if (IsThreadLocked())
            {
                throw new LockRecursionException();
            }

            Monitor.Enter(_lock);
            _lockedThread = Thread.CurrentThread;
            return new WatchLock(this);
        }

        private bool IsThreadLocked()
        {
            return Thread.CurrentThread.Equals(_lockedThread);
        }

        public void Exit()
        {
            EnsureNotDisposed();
            Monitor.Exit(_lock);
            _lockedThread = null;
        }

        private void EnsureNotDisposed()
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException("ExclusiveLockStrategy");
            }
        }
    }
}
