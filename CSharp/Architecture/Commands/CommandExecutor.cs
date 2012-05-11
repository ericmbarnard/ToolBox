using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Architecture.Commands
{
    /// <summary>
    /// Very basic example implementation of a Command Executor
    /// </summary>
    public class CommandExecutor
    {
        public void ExecuteCommand(Command cmd)
        {
            if (cmd == null)
                throw new ArgumentNullException("cmd");

            cmd.Execute();
        }

        public TResult ExecuteCommand<TResult>(Command<TResult> cmd)
        {
            if (cmd == null)
                throw new ArgumentNullException("cmd");

            cmd.Execute();

            return cmd.Result;
        }
    }
}
