using System;
using OrderListWPF.DAL.Model;

namespace OrderListWPF.GUI.Common.EventsData
{
    /// <summary>
    /// Event args for order events
    /// </summary>
    public class OrderEventArgs : EventArgs
    {
        public Order Order { get; private set; }
        public OrderEventType EventType { get; private set; }

        public OrderEventArgs(Order order, OrderEventType eventType)
        {
            Order = order;
            EventType = eventType;
        }

    }
    /// <summary>
    /// Order event delegate
    /// </summary>
    public delegate void OrderEvent(object sender, OrderEventArgs e);
}
