namespace Architecture.BackgroundTasks
{
    /// <summary>
    /// Wrapper around different Queuing mechanisms for <see cref="BackgroundTask"/>s
    /// Threadsafe
    /// </summary>
    public interface IBackgroundTaskQueue
    {
        /// <summary>
        /// Adds an item to the Queue
        /// </summary>
        /// <param name="task">A <see cref="BackgroundTask"/></param>
        void Enqueue(BackgroundTask task);

        /// <summary>
        /// Removes the first item from the Queue
        /// </summary>
        /// <returns>A <see cref="BackgroundTask"/></returns>
        BackgroundTask Dequeue();

        /// <summary>
        /// Accesses the first item from the Queue without Dequeuing it
        /// </summary>
        /// <returns>A <see cref="BackgroundTask"/></returns>
        BackgroundTask Peek();

        /// <summary>
        /// Indicates if the Queue is Empty
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// Hook for populating the Queue from an External Datasource if necessary
        /// </summary>
        void Initialize();
    }
}
