using Microsoft.VisualStudio.TestTools.UnitTesting;

using Architecture.Queries;

namespace Architecture.Tests.Queries
{
    #region Setup

    public class TestQuery : Query<int>
    {
        public override void Execute()
        {
            Result = 1;
        }
    }

    #endregion

    [TestClass]
    public class QueryExecutorTests
    {
        [TestMethod]
        public void Executing_A_Query_Works()
        {
            var executor = new QueryExecutor();
            var qry = new TestQuery();

            var result = executor.Query(qry);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Overriding_Query_Execution_Works()
        {
            var executor = new QueryExecutor { ExecutorOverride = q => 2 };

            var qry = new TestQuery();

            var result = executor.Query(qry);

            Assert.AreEqual(2, result);
        }
    }
}
