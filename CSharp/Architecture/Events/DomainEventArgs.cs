using System;

namespace Architecture.Events
{
    /// <summary>
    /// Marker Class for Arguments that will be passed to Event Handling code
    /// </summary>
    public abstract class DomainEventArgs : EventArgs
    {
        /// <summary>
        /// Indicates that further event processing should not continue
        /// </summary>
        public bool Cancel { get; set; }
    }
}
