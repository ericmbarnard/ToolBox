using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Architecture.Commands
{
    /// <summary>
    /// Performs Write Operations Synchronously
    /// </summary>
    public abstract class Command
    {
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
