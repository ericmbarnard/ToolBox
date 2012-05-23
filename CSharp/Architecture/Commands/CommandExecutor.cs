using System;

namespace Architecture.Commands
{
    /// <summary>
    /// Very basic example implementation of a Command Executor
    /// </summary>
    public class CommandExecutor
    {
        /// <summary>
        /// Optional handler for Command Execution. Useful for Unit Testing or providing very special logic.
        /// </summary>
        public Func<Command, object> ExecutorOverride { get; set; }

        /// <summary>
        /// Executes a <see cref="Architecture.Commands.Command"/>
        /// </summary>
        /// <param name="cmd">The Command To Execute</param>
        public void ExecuteCommand(Command cmd)
        {
            if (cmd == null)
                throw new ArgumentNullException("cmd");

            if (ExecutorOverride != null)
            {
                ExecutorOverride(cmd);
                return;
            }

            cmd.Execute();
        }

        /// <summary>
        /// Executes a <see cref="Architecture.Commands.Command"/>
        /// </summary>
        /// <param name="cmd">The Command To Execute</param>
        public TResult ExecuteCommand<TResult>(Command<TResult> cmd)
        {
            if (cmd == null)
                throw new ArgumentNullException("cmd");

            if (ExecutorOverride != null)
            {
                return (TResult) ExecutorOverride(cmd);
            }

            cmd.Execute();

            return cmd.Result;
        }
    }
}
