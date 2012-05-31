using System;
using System.Linq;
using Munq;

namespace Architecture.Events
{
    /// <summary>
    /// Manages the publishing of one-way events
    /// </summary>
    /// <remarks>
    /// Thanks to http://www.udidahan.com/2009/06/14/domain-events-salvation/
    /// </remarks>
    public class DomainEventExecutor
    {
        private static IDependencyResolver _container;
        private static readonly object _locker = new object();

        public DomainEventExecutor()
        {
            
        }

        public DomainEventExecutor(IDependencyResolver container)
        {
            if(container == null)
                throw new ArgumentNullException("container");

            Container = container;
        }

        /// <summary>
        /// Allows for explicit overriding of the event handling logic - useful for Unit Testing
        /// Or preventing standard handler execution logic from running
        /// </summary>
        public Action<DomainEventArgs> HandlerOverride { get; set; }

        /// <summary>
        /// The IoC Container
        /// </summary>
        public IDependencyResolver Container 
        { 
            get { return _container; } 
            set
            {
                if (value == null)
                    return;

                lock (_locker)
                {
                    _container = value;
                }
            } 
        } 
     
        /// <summary>
        /// Raises an Event to be handled domain-wide
        /// </summary>
        /// <typeparam name="T">The typeof <see cref="DomainEventArgs"/> args to handle</typeparam>
        /// <param name="args">The <see cref="DomainEventArgs"/> args to handle</param>
        public void RaiseEvent<T>(T args) where T : DomainEventArgs
        {

            if (HandlerOverride != null)
            {
                HandlerOverride(args);
                return;
            }

            // kind of hard to do anything more without this...
            if (Container == null)
                return;

            // make sure we grab all the handlers and then sort them
            // so that we execute them in priority order 99 -> 0
            var handlers = Container.ResolveAll<DomainEventHandler<T>>()
                                    .OrderByDescending(h => h.Priority)
                                    .ToList();

            foreach (var handler in handlers)
            {
                handler.Handle(args);
                    
                // if an event is cancelled, we need to stop and not go any further
                if (args.Cancel)
                    return;
            }
        }
    }
}
