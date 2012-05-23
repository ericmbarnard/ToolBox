using System.Collections.Concurrent;

namespace Architecture.BackgroundTasks
{
    public class BackgroundTaskMemoryQueue : IBackgroundTaskQueue
    {
        private ConcurrentQueue<BackgroundTask> _taskQueue = new ConcurrentQueue<BackgroundTask>();
        
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
            if (_taskQueue != null) return;

            _taskQueue = new ConcurrentQueue<BackgroundTask>();
            
        }
        
        public bool IsEmpty
        {
            get { return _taskQueue.IsEmpty; }
        }
    }
}
