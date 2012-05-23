using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Architecture.Queries;

namespace Architecture.Tests.Queries
{
    [TestClass]
    public class Query_Tests
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

        [TestMethod]
        public void Basic_Query_Test()
        {
            var qry = new TestQuery();

            qry.Execute();

            Assert.AreEqual(1, qry.Result);
        }

        [TestMethod]
        public void Basic_Query_Failure_Test()
        {
            var qry = new TestQuery();

            // ints default to zero
            Assert.AreEqual(0, qry.Result);
        }
    }
}
