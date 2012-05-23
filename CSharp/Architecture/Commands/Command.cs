using System;
using Architecture.Queries;

namespace Architecture.Commands
{
    /// <summary>
    /// Performs Write Operations Synchronously
    /// </summary>
    public abstract class Command
    {
        /// <summary>
        /// Overide handler for Query Execution
        /// </summary>
        public Func<Query, object> QueryExecutor;

        /// <summary>
        /// Utility Method for executing queries inside of a Command
        /// </summary>
        /// <typeparam name="T">The Query Result Type</typeparam>
        /// <param name="qry">The Query to Execute</param>
        /// <returns>The Query Result</returns>
        public T Query<T>(Query<T> qry)
        {
            if (QueryExecutor != null)
            {
                return (T) QueryExecutor(qry);
            }

            var executor = new QueryExecutor();
            return executor.Query(qry);
        }

        /// <summary>
        /// The logic for executing the command
        /// </summary>
        public abstract void Execute();
    }

    /// <summary>
    /// Performs Write Operations Synchronously and Returns a Result
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public abstract class Command<TResult> : Command
    {
        /// <summary>
        /// The Result of an Executed Command
        /// </summary>
        public TResult Result { get; protected set; }
    }
}
