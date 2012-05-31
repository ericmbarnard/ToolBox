using System.Collections.Concurrent;

namespace Architecture.BackgroundTasks
{
    public class BackgroundTaskMemoryQueue : IBackgroundTaskQueue
    {
        private readonly ConcurrentQueue<BackgroundTask> _taskQueue;

        #region Ctor

        public BackgroundTaskMemoryQueue()
        {
            _taskQueue = new ConcurrentQueue<BackgroundTask>();
        }

        #endregion

        public void Enqueue(BackgroundTask task)
        {
            _taskQueue.Enqueue(task);
        }

        public BackgroundTask Dequeue()
        {
            BackgroundTask task;

            return _taskQueue.TryDequeue(out task) ? task : null;
        }

        public BackgroundTask Peek()
        {
            BackgroundTask task;

            return _taskQueue.TryPeek(out task) ? task : null;
        }

        public void Initialize()
        {
            // nothing to implement for this specific queue
        }
        
        public bool IsEmpty
        {
            get { return _taskQueue.IsEmpty; }
        }
    }
}
