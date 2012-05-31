using System.Threading.Tasks;
using System;

namespace Architecture.BackgroundTasks
{
    /// <summary>
    /// Execution Logic for Background Tasks
    /// </summary>
    /// <remarks>
    /// Thanks Ayende https://github.com/fitzchak/RaccoonBlog/blob/master/HibernatingRhinos.Loci.Common/Tasks/TaskExecutor.cs
    /// </remarks>
    public class BackgroundTaskExecutor
    {
        private readonly IBackgroundTaskQueue _taskQueue;
        private readonly object _locker = new object();
        private bool _initialized;
        private bool _running;

        #region Ctor

        public BackgroundTaskExecutor()
            : this(new BackgroundTaskMemoryQueue())
        {
        }

        public BackgroundTaskExecutor(IBackgroundTaskQueue taskQueue)
        {
            if (taskQueue == null)
                throw new ArgumentNullException("taskQueue");

            _taskQueue = taskQueue;
            _taskQueue.Initialize();
            _initialized = true;
        } 
        #endregion

        public void Initialize()
        {
            if (_initialized)
                return;

            lock (_locker)
            {
                if (!_initialized)
                    _taskQueue.Initialize();
                _initialized = true;
            }
        }

        public void QueueTask(BackgroundTask task)
        {
            Initialize();
            _taskQueue.Enqueue(task);
        }

        public void StartExecuting()
        {
            Initialize();

            if (_running)
                return;

            lock (_locker)
            {
                if (_running)
                    return;

                _running = true;

                // kick off the queue processing on another thread so that we can call
                // this method cheaply
                Task.Factory.StartNew(ProcessQueue, TaskCreationOptions.LongRunning);
            }
        }

        public void ProcessQueue()
        {
            while (!_taskQueue.IsEmpty)
            {
                var task = _taskQueue.Dequeue();
                ExecuteTask(task);
            }

            // make sure we tell it that we're done running
            lock (_locker)
            {
                if (!_running)
                    return;

                _running = false;
            }
        }

        private void ExecuteTask(BackgroundTask task)
        {
            // ripe for more logic here, but this is a toolkit...
            // ideally we would probably be supplying open db connections
            // or db contexts and letting the task use those...
            task.Run();
        }
    }
}
