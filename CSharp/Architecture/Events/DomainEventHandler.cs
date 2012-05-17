using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Architecture.Queries;
using Architecture.Commands;

namespace Architecture.Events
{
    /// <summary>
    /// Defines a Handler for Domain Events
    /// </summary>
    /// <typeparam name="T">The <see cref="IDomainEvent"/> Type to be Handled</typeparam>
    public abstract class DomainEventHandler<T> where T : IDomainEvent
    {
        /// <summary>
        /// Override handler for Query Execution
        /// </summary>
        public Func<Query, object> QueryExecutor;

        /// <summary>
        /// Override handler for Command Execution
        /// </summary>
        public Func<Command, object> CommandExecutor;

        /// <summary>
        /// Utility Method for executing queries inside of a Command
        /// </summary>
        /// <typeparam name="T">The Query Result Type</typeparam>
        /// <param name="qry">The Query to Execute</param>
        /// <returns>The Query Result</returns>
        public T Query<T>(Query<T> qry)
        {
            if (QueryExecutor == null)
            {
                var executor = new QueryExecutor();
                return executor.Query(qry);
            }
            else
            {
                return (T)QueryExecutor(qry);
            }
        }

        /// <summary>
        /// Utility Method for executing commands inside of a Domain Event Handler
        /// </summary>
        /// <typeparam name="TResult">The Command Result</typeparam>
        /// <param name="cmd">The Command to execute</param>
        /// <returns>The Result of the Command</returns>
        public TResult ExecuteCommand<TResult>(Command<TResult> cmd)
        {
            if (CommandExecutor == null)
            {
                var executor = new CommandExecutor();
                return executor.ExecuteCommand(cmd);
            }
            else
            {
                return (TResult)CommandExecutor(cmd);
            }
        }

        /// <summary>
        /// The logic to be executed by the Handler
        /// </summary>
        /// <param name="args">The <see cref="IDomainEvent"/> to be handled</param>
        public abstract void Handle(T args);
    }
}
