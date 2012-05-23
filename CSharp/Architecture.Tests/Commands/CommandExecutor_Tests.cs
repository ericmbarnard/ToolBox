using Microsoft.VisualStudio.TestTools.UnitTesting;
using Architecture.Commands;

namespace Architecture.Tests.Commands
{
    [TestClass]
    public class CommandExecutor_Tests
    {
        #region Setup

        public class TestCommand : Command<int>
        {
            public override void Execute()
            {
                Result = 1;
            }
        }

        #endregion

        [TestMethod]
        public void Basic_Execution_Works()
        {
            var executor = new CommandExecutor();
            var cmd = new TestCommand();

            var result = executor.ExecuteCommand(cmd);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Overriding_Execution_Works()
        {
            var executor = new CommandExecutor();
            var cmd = new TestCommand();

            executor.ExecutorOverride = c => 2;

            var result = executor.ExecuteCommand(cmd);

            Assert.AreEqual(2, result);
        }
    }
}
