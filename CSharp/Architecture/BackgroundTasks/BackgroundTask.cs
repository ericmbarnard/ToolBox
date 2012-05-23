using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Architecture.BackgroundTasks
{
    public abstract class BackgroundTask
    {
        /// <summary>
        /// Overrideable Error Handling
        /// </summary>
        /// <param name="e">The Thrown Exception</param>
        public virtual void OnError(Exception e)
        {
        }

        /// <summary>
        /// Logic for the job to be implemented by all inheriting classes
        /// </summary>
        public abstract void Execute();

        /// <summary>
        /// Run Method wraps the Execute method with Error Handling.
        /// This should not be called by anything but the BackgroundTaskExecutor
        /// </summary>
        /// <returns>True if no Exceptions were thrown</returns>
        public bool Run()
        {
            try
            {
                Execute();
                return true;
            }
            catch (Exception e)
            {
                OnError(e);
                return false;
            }
        }
    }
}
