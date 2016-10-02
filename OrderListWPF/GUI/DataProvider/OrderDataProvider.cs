using System;
using System.Collections.Generic;
using System.Linq;
using OrderListWPF.DAL.Enum;
using OrderListWPF.DAL.Model;
using OrderListWPF.GUI.Common.EventsData;

namespace OrderListWPF.GUI.DataProvider
{
    /// <summary>
    /// OrderDataProvider
    /// </summary>
    public class OrderDataProvider : IOrderDataProvider
    {
        // how much orders generated in initialization
        private readonly int _startOrdersCount = 5;

        #region IOrderDataProvider implementation

        /// <summary>
        /// Notifies subscibers about order's changes, pass changed order and operation type
        /// </summary>
        public event OrderEvent OrdersChanged;

        /// <summary>
        /// Changes order status to Complete and raise OrdersChanged event
        /// </summary>
        /// <param name="executedOrder">order to execute</param>
        public void ExecuteOrder(Order executedOrder)
        {
            if (_orderList.Count == 0)
                return;

            executedOrder = _orderList.FirstOrDefault(o => o.Id == executedOrder.Id);

            if (executedOrder == null)
                return;

            executedOrder.OrderStatus = OrderStatusType.Complete;
            OrdersChanged?.Invoke(this, new OrderEventArgs(executedOrder, OrderEventType.Complete));
        }

        /// <summary>
        /// Generates new order
        /// </summary>
        public void GenerateOrder()
        {
            var newOrder = AddRandomOrder($"Order{DateTime.Now.ToString("ddMM_HHmmss")}");
            OrdersChanged?.Invoke(this, new OrderEventArgs(newOrder, OrderEventType.AddNew));
        }

        /// <summary>
        /// Returns a specified number of active orders
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public IEnumerable<Order> GetActiveOrders(int count = 0)
        {
            var list = _orderList.Where(o => o.OrderStatus == OrderStatusType.Active)
                                 .OrderByDescending(o => o.CreatingTime)
                                 .ThenByDescending(o => o.Id);

            if (count == 0 || list.Count() < count)
                return list;
            else
                return list.Take(count);
        }

        #endregion

        #region Ctor and initializing
        public OrderDataProvider()
        {
            Inizialize();
        }

        private void Inizialize()
        {
            _orderList = new List<Order>();
            for (var i = 0; i < _startOrdersCount; i++)
            {
                AddRandomOrder($"Order{DateTime.Now.ToString("ddMM_HHmmss")}");
            }
        }
        #endregion


        #region Orders Source imitation 

        // orders source list with all generated items
        private List<Order> _orderList;
        // pseudo id sequence:)
        private int _currentId = 0;


        Random random = new Random();

        /// <summary>
        /// Adds new random generated order to source list
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private Order AddRandomOrder(string tag = "")
        {
            _currentId++;
            var o = new Order(
                _currentId,
                (decimal)random.NextDouble() * random.Next(-1, 2) * (100 - 2) + 2,
                (OrderType)random.Next(0, 2),
                random.NextDouble() * (1500 - 254) + 254,
                random.NextDouble() * (1000 - 154) + 154,
                random.NextDouble() * (1200 - 214) + 214,
                tag,
                (OrderToProcessAs)random.Next(0, 2),
                OrderStatusType.Active);

            if (_orderList.Count(x => x.Id == o.Id) > 0)
                throw new ArgumentException("Repeat ID");
            _orderList.Add(o);

            return o;
        }
        #endregion        
    }
}
