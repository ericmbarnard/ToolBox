using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Architecture.Events;

namespace Architecture.Tests.Events
{
    [TestClass]
    public class DomainEventArgs_Tests
    {
        #region Setup

        public class TestArgs : DomainEventArgs
        {
            public int TestProp { get; set; }
        }

        #endregion

        [TestMethod]
        public void Basic_DomainEventArgs_Test()
        {
            // is this really needed? ... probably not,
            // but it does lock-in my api design, and I think 
            // thats a good thing for right now
            var args = new TestArgs
                           {
                               TestProp = 1, 
                               Cancel = true
                           };
            
            Assert.IsTrue(args.Cancel);
            Assert.AreEqual(1, args.TestProp);
        }
    }
}
