using Microsoft.VisualStudio.TestTools.UnitTesting;
using Architecture.Commands;

namespace Architecture.Tests.Commands
{
    [TestClass]
    public class Command_Tests
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
        public void Basic_Query_Test()
        {
            var cmd = new TestCommand();

            cmd.Execute();

            Assert.AreEqual(1, cmd.Result);
        }

        [TestMethod]
        public void Basic_Query_Failure_Test()
        {
            var cmd = new TestCommand();

            // ints default to zero
            Assert.AreEqual(0, cmd.Result);
        }
    }
}
