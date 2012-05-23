using Architecture.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Munq;

namespace Architecture.Tests.Events
{
    [TestClass]
    public class DomainEventExecutor_Tests
    {
        #region Setup

        // args
        public class TestArgs : DomainEventArgs
        {
            public int TestProp { get; set; }
        }

        // handlers
        public class TestHandlerOne : DomainEventHandler<TestArgs>
        {
            public override void Handle(TestArgs args)
            {
                args.TestProp = 1;
            }
        }

        public class CancelHandler : DomainEventHandler<TestArgs>
        {
            public CancelHandler()
            {
                Priority = 2;
            }

            public override void Handle(TestArgs args)
            {
                args.Cancel = true;
            }
        }

        #endregion

        [TestMethod]
        public void Basic_EventExecution_Works()
        {
            var container = new IocContainer();
            container.Register(c => new TestHandlerOne());

            var executor = new DomainEventExecutor(container);
            
            var args = new TestArgs();

            executor.RaiseEvent(args);

            Assert.AreEqual(1, args.TestProp);
        }

        [TestMethod]
        public void Cancelling_EventExecution_Works()
        {
            var container = new IocContainer();
            container.Register(c => new TestHandlerOne());
            container.Register(c => new CancelHandler());

            var executor = new DomainEventExecutor(container);

            var args = new TestArgs();

            executor.RaiseEvent(args);

            Assert.IsTrue(args.Cancel);
            Assert.AreEqual(0, args.TestProp);
        }
    }
}
