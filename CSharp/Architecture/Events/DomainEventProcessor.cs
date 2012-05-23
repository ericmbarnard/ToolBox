using System;
using System.Collections.Generic;
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
    public static class DomainEventProcessor
    {
        /// <summary>
        /// Extra Actions to be executed in front of the Handlers
        /// </summary>
        [ThreadStatic]
        private static List<Delegate> _actions;
     
        /// <summary>
        /// The IoC Container
        /// </summary>
        public static IDependencyResolver Container { get; set; } //as before
     
        /// <summary>
        /// Allows an explicit registration of an Event Handling lambda that will execute before
        /// other handlers
        /// </summary>
        /// <typeparam name="T">The <see cref="DomainEventArgs"/>that will be passed to the handler</typeparam>
        /// <param name="handler">The handling lambda to execute</param>
        public static void Register<T>(Action<T> handler) where T : DomainEventArgs
        {
            if (_actions == null)
                _actions = new List<Delegate>();
     
            _actions.Add(handler);
        }
     
        /// <summary>
        /// Utility method for clearing explicitly registered handlers
        /// </summary>
        public static void ClearHandlers()
        {
            _actions = null;
        }
     
        /// <summary>
        /// Raises an Event to be handled domain-wide
        /// </summary>
        /// <typeparam name="T">The typeof <see cref="DomainEventArgs"/> args to handle</typeparam>
        /// <param name="args">The <see cref="DomainEventArgs"/> args to handle</param>
        public static void RaiseEvent<T>(T args) where T : DomainEventArgs
        {
            bool cancelled = false;

            if (Container != null)
            {
                List<DomainEventHandler<T>> handlers = Container.ResolveAll<DomainEventHandler<T>>()
                                                                .OrderByDescending(h => h.Priority)
                                                                .ToList();

                foreach (var handler in handlers)
                {
                    handler.Handle(args);
                    
                    // if an event is cancelled, we need to stop and not go any further
                    cancelled = args.Cancel;

                    if (cancelled)
                    {
                        return;
                    }
                }
            }

            if (_actions != null)
            {
                foreach (var action in _actions)
                {
                    if (action is Action<T>)
                        ((Action<T>) action)(args);

                    // if an event is cancelled, we need to stop and not go any further
                    cancelled = args.Cancel;

                    if (cancelled)
                    {
                        return;
                    }
                }
            }
        }
    }
}
