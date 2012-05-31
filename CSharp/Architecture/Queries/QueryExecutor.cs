using System;

namespace Architecture.Queries
{
    /// <summary>
    /// Class that handles the Execution of <see cref="Architecture.Queries.Query"/> objects
    /// </summary>
    public class QueryExecutor
    {
        /// <summary>
        /// Optional handler for Query Execution. Useful for Unit Testing or providing very special logic.
        /// </summary>
        public Func<Query, object> ExecutorOverride { get; set; }
        
        /// <summary>
        /// Executes a <see cref="Architecture.Queries.Query"/>
        /// </summary>
        /// <typeparam name="T">The Return Type of the Query</typeparam>
        /// <param name="qry">The Query to Execute</param>
        /// <returns>The Query Result</returns>
        public T Query<T>(Query<T> qry)
        {
            if (qry == null)
                throw new ArgumentNullException("qry");

            if (ExecutorOverride != null)
            {
                return (T)ExecutorOverride(qry);
            }
            
            qry.Execute();
            
            return qry.Result;
        }
    }
}
