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
    public class LockStrategy : IDisposable, ILockStrategy
    {
        private ReaderWriterLockSlim _lock;

        public LockStrategy() : this(LockRecursionPolicy.NoRecursion)
        {
        }

        public LockStrategy(LockRecursionPolicy recursionPolicy)
        {
            _lock = new ReaderWriterLockSlim(recursionPolicy);
        }

        #region ILockStrategy Members

        public ILock GetReadLock()
        {
            return GetReadLock(TimeSpan.FromMilliseconds(-1));
        }

        public ILock GetReadLock(TimeSpan timeout)
        {
            EnsureNotDisposed();
            if (_lock.TryEnterReadLock(timeout))
            {
                return new LockReaderWriter(_lock);
            }
            else
            {
                throw new ApplicationException("LockStrategy.GetReadLock timed out");
            }
        }

        public ILock GetWriteLock()
        {
            return GetWriteLock(TimeSpan.FromMilliseconds(-1));
        }

        public ILock GetWriteLock(TimeSpan timeout)
        {
            EnsureNotDisposed();
            if (_lock.TryEnterWriteLock(timeout))
            {
                return new LockReaderWriter(_lock);
            }
            else
            {
                throw new ApplicationException("LockStrategy.GetWriteLock timed out");
            }
        }

        public bool ThreadCanRead
        {
            get
            {
                EnsureNotDisposed();
                return _lock.IsReadLockHeld || _lock.IsWriteLockHeld;
                //todo uncomment if upgradelock is used OrElse _lock.IsUpgradeableReadLockHeld
            }
        }

        public bool ThreadCanWrite
        {
            get
            {
                EnsureNotDisposed();
                return _lock.IsWriteLockHeld;
            }
        }

        public bool SupportsConcurrentReads
        {
            get
            {
                return true;
            }
        }

        #endregion

        #region "IDisposable Support"

        private bool _isDisposed;

        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void EnsureNotDisposed()
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException("LockStrategy");
            }
        }

        // To detect redundant calls

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    //dispose managed state (managed objects).
                }

                _lock.Dispose();
                _lock = null;
            }
            _isDisposed = true;
        }

        ~LockStrategy()
        {
            // Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(false);
        }

        // This code added by Visual Basic to correctly implement the disposable pattern.

        #endregion
    }
}
