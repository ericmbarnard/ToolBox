using System.Threading.Tasks;
using System.Web.Hosting;

namespace Architecture.BackgroundTasks
{
    /// <summary>
    /// Execution Logic for Background Tasks
    /// </summary>
    /// <remarks>
    /// Thanks Ayende https://github.com/fitzchak/RaccoonBlog/blob/master/HibernatingRhinos.Loci.Common/Tasks/TaskExecutor.cs
    /// </remarks>
    public static class BackgroundTaskExecutor
    {
        private static readonly IBackgroundTaskQueue _taskQueue = new BackgroundTaskMemoryQueue();
        private static readonly object _locker = new object();
        private static bool _initialized;
        private static bool _running;

        public static void QueueTask(BackgroundTask task)
        {
            Initialize();
            _taskQueue.Enqueue(task);
        }

        public static void StartExecuting()
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

        public static void ProcessQueue()
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

        public static void ExecuteTask(BackgroundTask task)
        {
            // ripe for more logic here, but this is a toolkit...
            // ideally we would probably be supplying open db connections
            // or db contexts and letting the task use those...
            task.Run();
        }

        public static void Initialize()
        {
            if (_initialized)
                return;

            lock (_locker)
            {
                if(!_initialized)
                    _taskQueue.Initialize();
                _initialized = true;
            }
        }

    }
}
