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
    /// <typeparam name="T">The <see cref="DomainEventArgs"/> Type to be Handled</typeparam>
    public abstract class DomainEventHandler<T> where T : DomainEventArgs
    {
        protected DomainEventHandler()
        {
            QueryExecutor = new QueryExecutor();
            CommandExecutor = new CommandExecutor();
            Priority = 0;
        }

        /// <summary>
        /// The logic to be executed by the Handler
        /// </summary>
        /// <param name="args">The <see cref="DomainEventArgs"/> to be handled</param>
        public abstract void Handle(T args);

        /// <summary>
        /// The priority order in which the handlers will be executed. 
        /// Positive numbers increase Priority, Zero and Negatives decrease priority
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// The execution logic for Queries
        /// </summary>
        public QueryExecutor QueryExecutor { get; private set; }

        /// <summary>
        /// The execution logic for Commands
        /// </summary>
        public CommandExecutor CommandExecutor { get; private set; }

        /// <summary>
        /// Executes a <see cref="Query"/>
        /// </summary>
        /// <typeparam name="TResult">The typeof Result from the Query</typeparam>
        /// <param name="qry">The Query to Execute</param>
        /// <returns>A Query Result</returns>
        protected TResult Query<TResult>(Query<TResult> qry)
        {
            return QueryExecutor.Query(qry);
        }

        /// <summary>
        /// Executes a <see cref="Command"/>
        /// </summary>
        /// <typeparam name="TResult">The typeof Result from the Command</typeparam>
        /// <param name="command">The Command to Execute</param>
        /// <returns>A Command Result</returns>
        protected TResult ExecuteCommand<TResult>(Command<TResult> command)
        {
            return CommandExecutor.ExecuteCommand(command);
        }
    }
}
