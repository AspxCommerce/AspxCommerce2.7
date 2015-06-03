#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using System.Net;
using System.Threading.Tasks;
#endregion

namespace SageFrame.Scheduler
{
    public enum ScheduleStatus
    {
        NOT_SET,
        WAITING_FOR_OPEN_THREAD,
        RUNNING_EVENT_SCHEDULE,
        RUNNING_TIMER_SCHEDULE,
        RUNNING_REQUEST_SCHEDULE,
        WAITING_FOR_REQUEST,
        SHUTTING_DOWN,
        STOPPED
    }


    public enum RunningMode
    {

        Hourly,
        Daily,
        Weekly,
        WeekNumber,
        Calendar,
        Once
    }

    public delegate void TaskProcessing(TaskBase task);
    public delegate void TaskCompleted(TaskBase task);
    public delegate void TaskErrored(TaskBase task, Exception exception);
    public delegate void TaskStarted(TaskBase task);

    public class Scheduler
    {

        private static int _maxThreadCount;
        private static int ActiveThreadCount;
        private static bool ForceReloadSchedule;
        private bool _threadPoolInitialized = false;

        private static bool _debug;

        private static int _numberOfProcessGroups;

        private static readonly TaskList<ScheduleHistory> ScheduleQueue;
        private static readonly TaskList<ScheduleHistory> ScheduleInProgress;


        private static TaskGroup[] arrProcessGroup;


        private static int _readerTimeouts;
        private static int _writerTimeouts;
        private static readonly TimeSpan LockTimeout = TimeSpan.FromSeconds(45);
        private static readonly ReaderWriterLock StatusLock = new ReaderWriterLock();
        private static ScheduleStatus _status = ScheduleStatus.STOPPED;

        public static bool KeepThreadAlive = true;
        public static bool KeepRunning = true;

        static Scheduler()
        {
            var lockStrategy = new LockStrategy(LockRecursionPolicy.SupportsRecursion);

            ScheduleQueue = new TaskList<ScheduleHistory>(lockStrategy);
            ScheduleInProgress = new TaskList<ScheduleHistory>(lockStrategy);
        }

        public Scheduler(int maxThreads)
            : this(false, maxThreads)
        {
        }

        public Scheduler(bool boolDebug, int maxThreads)
        {
            _debug = boolDebug;
            if (!_threadPoolInitialized)
            {
                InitializeThreadPool(maxThreads);
            }
        }


        public static int FreeThreads
        {
            get
            {
                return _maxThreadCount - ActiveThreadCount;
            }
        }


        private static void AddToScheduleInProgress(ScheduleHistory scheduleHistoryItem)
        {
            if (!(ScheduleInProgressContains(scheduleHistoryItem)))
            {
                try
                {
                    using (ScheduleInProgress.GetWriteLock(LockTimeout))
                    {
                        if (!(ScheduleInProgressContains(scheduleHistoryItem)))
                        {
                            ScheduleInProgress.AddItem(scheduleHistoryItem);
                        }
                    }
                }
                catch (ApplicationException ex)
                {
                    // The writer lock request timed out.
                    Interlocked.Increment(ref _writerTimeouts);
                    ErrorLogger.SchedulerProcessException(ex);
                }
            }
        }

        private static int GetProcessGroup()
        {

            var r = new Random();
            return r.Next(0, _numberOfProcessGroups - 1);
        }

        private static bool IsInProgress(Schedule scheduleItem)
        {
            try
            {
                using (ScheduleInProgress.GetReadLock(LockTimeout))
                {
                    return ScheduleInProgress.Any(si => si.ScheduleID == scheduleItem.ScheduleID);
                }
            }
            catch (ApplicationException ex)
            {

                Interlocked.Increment(ref _readerTimeouts);
                ErrorLogger.SchedulerProcessException(ex);
                return false;
            }
        }


        private static void RemoveFromScheduleInProgress(Schedule scheduleItem)
        {
            try
            {
                using (ScheduleInProgress.GetWriteLock(LockTimeout))
                {
                    ScheduleHistory item = ScheduleInProgress.Where(si => si.ScheduleID == scheduleItem.ScheduleID).SingleOrDefault();
                    if (item != null)
                    {
                        ScheduleInProgress.Remove(item);
                    }
                }
            }
            catch (ApplicationException ex)
            {

                Interlocked.Increment(ref _writerTimeouts);
                ErrorLogger.SchedulerProcessException(ex);
            }
        }

        private static bool ScheduleInProgressContains(ScheduleHistory scheduleHistoryItem)
        {
            try
            {
                using (ScheduleInProgress.GetReadLock(LockTimeout))
                {
                    return ScheduleInProgress.Any(si => si.ScheduleID == scheduleHistoryItem.ScheduleID);
                }
            }
            catch (ApplicationException ex)
            {
                Interlocked.Increment(ref _readerTimeouts);
                ErrorLogger.SchedulerProcessException(ex);
                return false;
            }
        }

        private static bool ScheduleQueueContains(ScheduleHistory objScheduleItem)
        {
            try
            {
                using (ScheduleQueue.GetReadLock(LockTimeout))
                {
                    return ScheduleQueue.Any(si => si.ScheduleID == objScheduleItem.ScheduleID);
                }
            }
            catch (ApplicationException ex)
            {
                Interlocked.Increment(ref _readerTimeouts);
                ErrorLogger.SchedulerProcessException(ex);
                return false;
            }
        }

        internal static bool IsInQueue(Schedule scheduleItem)
        {
            try
            {
                using (ScheduleQueue.GetReadLock(LockTimeout))
                {
                    return ScheduleQueue.Any(si => si.ScheduleID == scheduleItem.ScheduleID);
                }
            }
            catch (ApplicationException)
            {

                Interlocked.Increment(ref _readerTimeouts);
                return false;
            }
        }



        public static void AddToScheduleQueue(ScheduleHistory objScheduleHistory)
        {
            if (!ScheduleQueueContains(objScheduleHistory))
            {
                try
                {

                    using (ScheduleQueue.GetWriteLock(LockTimeout))
                    {

                        if (!ScheduleQueueContains(objScheduleHistory) &&
                            !IsInProgress(objScheduleHistory))
                        {

                            ScheduleQueue.AddItem(objScheduleHistory);
                        }
                    }
                }
                catch (ApplicationException ex)
                {
                    // The writer lock request timed out.
                    Interlocked.Increment(ref _writerTimeouts);
                    ErrorLogger.SchedulerProcessException(ex);
                }
            }
        }

        public static void FireEvents(bool Asynchronous)
        {

            using (ScheduleQueue.GetReadLock(LockTimeout))
            {
                int numToRun = ScheduleQueue.Count;
                int numRun = 0;

                foreach (ScheduleHistory objSchedule in ScheduleQueue)
                {
                    if (!KeepRunning)
                    {
                        return;
                    }

                    int ProcessGroup = GetProcessGroup();

                    //if (objSchedule.NextStart <= DateTime.Now &&
                    //    objSchedule.Enabled &&
                    //    !IsInProgress(objSchedule) &&
                    //    numRun < numToRun)
                    //{


                    if (objSchedule.StartDate != null)
                    {

                        string strStartDate = objSchedule.StartDate.Substring(0, objSchedule.StartDate.LastIndexOf("/") + 5);
                        DateTime? tmpDate = objSchedule.HistoryStartDate == null ?
                            Convert.ToDateTime(strStartDate + " " + objSchedule.StartHour + ":" + objSchedule.StartMin + ":00") :
                           objSchedule.HistoryStartDate;

                        if (((string.IsNullOrEmpty(objSchedule.NextStart) && objSchedule.HistoryStartDate == null && tmpDate <= DateTime.Now) ||
                                    (DateTime.Parse(objSchedule.NextStart) <= DateTime.Now && (string.IsNullOrEmpty(objSchedule.EndDate)
                                        || DateTime.Parse(objSchedule.EndDate) > DateTime.Now)))
                                       && !IsInProgress(objSchedule) && numRun < numToRun && objSchedule.Status != true)
                        {
                            objSchedule.ProcessGroup = ProcessGroup;

                            // if (Asynchronous)
                            arrProcessGroup[ProcessGroup].AddQueueUserWorkItem(objSchedule);
                            // else
                            // arrProcessGroup[ProcessGroup].RunSingleTask(objSchedule);

                            numRun += 1;
                        }
                    }
                    else
                    {
                        ErrorLogger.SchedulerProcessException(new Exception("Not started for task" + objSchedule.ScheduleName));
                    }
                }
            }
        }


        public static int GetActiveThreadCount()
        {
            return ActiveThreadCount;
        }

        public static int GetFreeThreadCount()
        {
            return FreeThreads;
        }

        public static int GetMaxThreadCount()
        {
            return _maxThreadCount;
        }


        public static TaskList<ScheduleHistory> GetScheduleInProgress()
        {
            var c = new TaskList<ScheduleHistory>();
            try
            {
                using (ScheduleInProgress.GetReadLock(LockTimeout))
                {
                    foreach (ScheduleHistory item in ScheduleInProgress)
                    {
                        c.AddItem(item);
                    }
                }
            }
            catch (ApplicationException ex)
            {

                Interlocked.Increment(ref _readerTimeouts);
                ErrorLogger.SchedulerProcessException(ex);
            }
            return c;
        }


        public static int GetScheduleInProgressCount()
        {
            try
            {
                using (ScheduleInProgress.GetReadLock(LockTimeout))
                {
                    return ScheduleInProgress.Count;
                }
            }
            catch (ApplicationException)
            {
                Interlocked.Increment(ref _readerTimeouts);

                return 0;
            }
        }


        public static TaskList<ScheduleHistory> GetScheduleQueue()
        {
            var c = new TaskList<ScheduleHistory>();
            try
            {
                using (ScheduleQueue.GetReadLock(LockTimeout))
                {
                    foreach (ScheduleHistory item in ScheduleQueue)
                    {
                        c.AddItem(item);
                    }
                }
                return c;
            }
            catch (ApplicationException ex)
            {
                Interlocked.Increment(ref _readerTimeouts);
                ErrorLogger.SchedulerProcessException(ex);
            }
            return c;
        }


        public static int GetScheduleQueueCount()
        {
            try
            {
                using (ScheduleQueue.GetReadLock(LockTimeout))
                {
                    return ScheduleQueue.Count;
                }
            }
            catch (ApplicationException)
            {
                Interlocked.Increment(ref _readerTimeouts);
                return 0;
            }
        }

        public static ScheduleStatus GetScheduleStatus()
        {
            try
            {
                StatusLock.AcquireReaderLock(LockTimeout);
                try
                {
                    return _status;
                }
                finally
                {
                    StatusLock.ReleaseReaderLock();
                }
            }
            catch (ApplicationException)
            {
                Interlocked.Increment(ref _readerTimeouts);
                return ScheduleStatus.NOT_SET;
            }
        }


        public static void Halt(string sourceOfHalt)
        {
           
            var currentStatus = GetScheduleStatus();
            if (currentStatus == ScheduleStatus.NOT_SET || currentStatus == ScheduleStatus.STOPPED)
            {
                return;
            }

            SetScheduleStatus(ScheduleStatus.SHUTTING_DOWN);


            KeepRunning = false;

            for (int i = 0; i <= 120; i++)
            {
                if (GetScheduleStatus() == ScheduleStatus.STOPPED)
                {
                    return;
                }
                Thread.Sleep(1000);
            }

            ActiveThreadCount = 0;
        }










        public static void RemoveFromScheduleQueue(Schedule scheduleItem)
        {
            try
            {
                using (ScheduleQueue.GetWriteLock(LockTimeout))
                {
                    ScheduleHistory item = ScheduleQueue.Where(si => si.ScheduleID == scheduleItem.ScheduleID).SingleOrDefault();
                    if (item != null)
                    {
                        ScheduleQueue.Remove(item);
                    }
                }
            }
            catch (ApplicationException ex)
            {
             
                Interlocked.Increment(ref _writerTimeouts);
                ErrorLogger.SchedulerProcessException(ex);
            }
        }

        public static void RemoveFromScheduleQueueByID(int ScheduleID)
        {
            try
            {
                using (ScheduleQueue.GetWriteLock(LockTimeout))
                {
                    ScheduleHistory item = ScheduleQueue.Where(si => si.ScheduleID == ScheduleID).SingleOrDefault();
                    if (item != null)
                    {
                        ScheduleQueue.Remove(item);
                    }
                }
            }
            catch (ApplicationException ex)
            {
                Interlocked.Increment(ref _writerTimeouts);
                ErrorLogger.SchedulerProcessException(ex);
            }
        }
        public static void RunEventSchedule()
        {
            try
            {

                ScheduleHistory objScheduleHistory = null;

                List<Schedule> schedules = SchedulerController.GetTasks();

                foreach (Schedule schedule in schedules)
                {
                    objScheduleHistory = new ScheduleHistory(schedule);
                    //check &&  (objScheduleHistory.Status == false || objScheduleHistory.Status == null) &&
                    if (!IsInQueue(objScheduleHistory) && !IsInProgress(objScheduleHistory) && (objScheduleHistory.Status == false || objScheduleHistory.Status == null) && objScheduleHistory.IsEnable)
                    {
                        objScheduleHistory.Status = null;
                        AddToScheduleQueue(objScheduleHistory);
                    }
                }

                while (GetScheduleQueueCount() > 0)
                {
                    SetScheduleStatus(ScheduleStatus.RUNNING_EVENT_SCHEDULE);

                    //Fire off the events that need running.
                    if (GetScheduleQueueCount() > 0)
                    {
                        FireEvents(true);
                    }


                    if (_writerTimeouts > 20 || _readerTimeouts > 20)
                    {
                        //Wait for 10 minutes so we don't fill up the logs
                        Thread.Sleep(TimeSpan.FromMinutes(10));
                    }
                    else
                    {
                        //Wait for 10 seconds to avoid cpu overutilization
                        Thread.Sleep(TimeSpan.FromSeconds(10));
                    }

                    if (GetScheduleQueueCount() == 0)
                    {
                        return;
                    }
                }
            }
            catch (Exception exc)
            {
                ErrorLogger.SchedulerProcessException(exc);
            }
        }

        public static void SetScheduleStatus(ScheduleStatus newStatus)
        {
            try
            {

                StatusLock.AcquireWriterLock(LockTimeout);
                try
                {
                    _status = newStatus;
                }
                finally
                {
                    StatusLock.ReleaseWriterLock();
                }
            }
            catch (ApplicationException ex)
            {
                Interlocked.Increment(ref _writerTimeouts);
                ErrorLogger.SchedulerProcessException(ex);
            }
        }

        public static void ReloadSchedule()
        {
            ForceReloadSchedule = true;
        }

        public static void Start()
        {
            for (int i = 0; i <= 30; i++)
            {
                Thread.Sleep(1000);
            }

            try
            {
                ActiveThreadCount = 0;



                while (KeepRunning)
                {
                    try
                    {


                        ForceReloadSchedule = false;
                        ScheduleHistory objScheduleHistory = null;

                        List<Schedule> schedules = SchedulerController.GetTasks();

                        foreach (Schedule schedule in schedules)
                        {
                            objScheduleHistory = new ScheduleHistory(schedule);
                            //check &&  (objScheduleHistory.Status == false || objScheduleHistory.Status == null) &&
                            if (!IsInQueue(objScheduleHistory) && !IsInProgress(objScheduleHistory) && (objScheduleHistory.Status == false || objScheduleHistory.Status == null) && objScheduleHistory.IsEnable)
                            {
                                objScheduleHistory.Status = null;
                                AddToScheduleQueue(objScheduleHistory);
                            }
                        }

                        DateTime lastQueueRefresh = DateTime.Now;
                        bool refreshQueueSchedule = false;

                        while (FreeThreads > 0 && KeepRunning && !ForceReloadSchedule)
                        {


                            if (GetScheduleQueueCount() > 0)
                            {
                                FireEvents(true);
                            }
                            if (KeepThreadAlive == false)
                            {
                                return;
                            }


                            if (_writerTimeouts > 20 || _readerTimeouts > 20)
                            {
                                if (KeepRunning)
                                {
                                    Thread.Sleep(TimeSpan.FromMinutes(10));
                                }
                                else
                                {
                                    return;
                                }
                            }
                            else
                            {
                                if (KeepRunning)
                                {
                                    Thread.Sleep(TimeSpan.FromSeconds(10));
                                }
                                else
                                {
                                    return;
                                }

                                if ((lastQueueRefresh.AddMinutes(10) <= DateTime.Now || ForceReloadSchedule) && FreeThreads == _maxThreadCount)
                                {
                                    refreshQueueSchedule = true;
                                    break;
                                }
                            }
                        }

                        if (KeepRunning)
                        {
                            if (refreshQueueSchedule == false)
                            {
                                SetScheduleStatus(ScheduleStatus.WAITING_FOR_OPEN_THREAD);
                                // Thread.Sleep(10000); //sleep for 10 seconds
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                    catch (Exception exc)
                    {

                        ErrorLogger.SchedulerProcessException(exc);
                        //sleep for 10 minutes
                        //Thread.Sleep(600000);
                    }
                }
            }
            finally
            {
                SetScheduleStatus(ScheduleStatus.STOPPED);
                KeepRunning = false;
                ActiveThreadCount = 0;

            }
        }



        public static void TaskCompleted(TaskBase task)
        {
            try
            {
                ScheduleHistory objScheduleHistory = task.TaskRecordItem;
                objScheduleHistory = task.TaskRecordItem;

                RemoveFromScheduleInProgress(objScheduleHistory);
                Interlocked.Decrement(ref ActiveThreadCount);

                objScheduleHistory.HistoryEndDate = DateTime.Now;

                objScheduleHistory.Status = true;
                objScheduleHistory.NextStart = SchedulerController.GetNextStart(objScheduleHistory);

                task.TaskRecordItem.ReturnText = "Successfully completed";

                SchedulerController.UpdateTaskHistory(objScheduleHistory);

                if (!string.IsNullOrEmpty(objScheduleHistory.NextStart))
                {
                    objScheduleHistory.HistoryStartDate = null;
                    objScheduleHistory.HistoryEndDate = null;
                    objScheduleHistory.ReturnText = "";
                    objScheduleHistory.ProcessGroup = -1;
                    objScheduleHistory.Status = false;
                    AddToScheduleQueue(objScheduleHistory);
                }
            }
            catch (Exception exc)
            {
                if (task.TaskRecordItem != null) ErrorLogger.SchedulerProcessException(exc);
            }
        }

        public static void TaskErrored(TaskBase task, Exception exception)
        {
            try
            {
                ScheduleHistory objScheduleHistoryItem = task.TaskRecordItem;

                RemoveFromScheduleInProgress(objScheduleHistoryItem);

                Interlocked.Decrement(ref ActiveThreadCount);

                objScheduleHistoryItem.HistoryEndDate = DateTime.Now;

                DateTime startedTime = objScheduleHistoryItem.HistoryStartDate ?? DateTime.Now;

                if (objScheduleHistoryItem.RetryTimeLapse > 0)
                {
                    switch (objScheduleHistoryItem.RetryFrequencyUnit)
                    {
                        case 1:
                            startedTime.AddSeconds(objScheduleHistoryItem.RetryTimeLapse);
                            break;
                        case 2:
                            startedTime.AddMinutes(objScheduleHistoryItem.RetryTimeLapse);
                            break;
                        case 3:
                            startedTime.AddHours(objScheduleHistoryItem.RetryTimeLapse);
                            break;
                        case 4:
                            startedTime.AddDays(objScheduleHistoryItem.RetryTimeLapse);
                            break;
                    }
                    objScheduleHistoryItem.NextStart = Convert.ToString(startedTime);
                }

                objScheduleHistoryItem.Status = false;
                string error = "";

                StringBuilder errorlog = new StringBuilder();

                errorlog.AppendLine("Error");
                if ((exception != null))
                {
                    error = exception.Message;
                }
                errorlog.AppendLine(error);

                task.TaskRecordItem.ReturnText = errorlog.ToString();

                SchedulerController.UpdateTaskHistory(objScheduleHistoryItem);

                if (!string.IsNullOrEmpty(objScheduleHistoryItem.NextStart) && objScheduleHistoryItem.RetryTimeLapse > 0)
                {
                    objScheduleHistoryItem.HistoryStartDate = null;
                    objScheduleHistoryItem.HistoryEndDate = null;
                    objScheduleHistoryItem.ReturnText = "";
                    objScheduleHistoryItem.ProcessGroup = -1;
                    AddToScheduleQueue(objScheduleHistoryItem);
                }
            }
            catch (Exception exc)
            {
                if (task.TaskRecordItem != null) ErrorLogger.SchedulerProcessException(exc);
            }
        }
        public static void TaskProgressing(TaskBase task)
        {
            try
            {

            }
            catch (Exception exc)
            {
                ErrorLogger.SchedulerProcessException(exc);
            }
        }


        public static void TaskStarted(TaskBase task)
        {

            bool ActiveThreadCountIncremented = false;
            try
            {
                task.TaskRecordItem.ThreadID = Thread.CurrentThread.GetHashCode();
                if (task.TaskRecordItem.Status != true)
                    task.TaskRecordItem.Status = false;
                task.TaskRecordItem.ReturnText = "Process Started";

                IPHostEntry entries = Dns.GetHostEntry(Dns.GetHostName());
                task.TaskRecordItem.Server = entries.AddressList[0].ToString();

                RemoveFromScheduleQueue(task.TaskRecordItem);
                AddToScheduleInProgress(task.TaskRecordItem);

                Interlocked.Increment(ref ActiveThreadCount);
                ActiveThreadCountIncremented = true;

                task.TaskRecordItem.HistoryStartDate = DateTime.Now;
                task.TaskRecordItem.ScheduleHistoryID = SchedulerController.AddTaskHistory(task.TaskRecordItem);
            }
            catch (Exception exc)
            {
                if (ActiveThreadCountIncremented)
                    Interlocked.Decrement(ref ActiveThreadCount);
                if (task.TaskRecordItem != null) ErrorLogger.SchedulerProcessException(exc);

            }

        }

        private void InitializeThreadPool(int maxThreads)
        {
            if (maxThreads == -1)
            {
                maxThreads = 1;
            }
            _numberOfProcessGroups = maxThreads;
            _maxThreadCount = maxThreads;
            for (int i = 0; i < _numberOfProcessGroups; i++)
            {
                Array.Resize(ref arrProcessGroup, i + 1);
                arrProcessGroup[i] = new TaskGroup();
            }
            _threadPoolInitialized = true;
        }
    }
}

