using System;
using OrderListWPF.DAL.Enum;
using OrderListWPF.DAL.Model;
using OrderListWPF.GUI.Common.Helpers;
using OrderListWPF.GUI.ViewModel.Base;

namespace OrderListWPF.GUI.ViewModel
{
    /// <summary>
    ///  View model for Order object
    /// </summary>
    public class OrderItemViewModel : BaseNotification, IOrderItemViewModel
    {
        /// <summary>
        /// Order item
        /// </summary>
        private readonly Order _item;

        #region Order's object members
        public int Id => _item.Id;
        public string ActionName => _item.Action.GetDescription();
        public decimal Quantity => _item.Quantity;
        public decimal Size => _item.Size;
        public string OrderTypeName => _item.OrderType.GetDescription();
        public double LimitPrice => _item.LimitPrice;
        public double StopPrice => _item.StopPrice;
        public double ExpectedTradePrice => _item.ExpectedTradePrice;
        public string Tag => _item.Tag;
        public string ToProcessAsName => _item.ToProcessAs.GetDescription();
        public DateTime CreatingTime => _item.CreatingTime;

        public OrderStatusType OrderStatus
        {
            get { return _item.OrderStatus; }
            set
            {
                _item.OrderStatus = value;
                if (value == OrderStatusType.Active && (DateTime.Now - CreatingTime).Seconds <= _newStatusLimitSeconds)
                    OrderViewStatus = OrdersViewStatusType.New;
                else
                    OrderViewStatus = (OrdersViewStatusType)value;
                NotifyPropertyChanged(nameof(OrderStatus));
            }
        }

        #endregion

        #region Additional members
        /// <summary>
        /// OrdersViewStatus have two additional states for highlighting: 
        /// New - when order's creation time is less then 10 second ago
        /// Compliting - when client knows that order is complete, but still can't delete him from source list
        /// </summary>
        private OrdersViewStatusType _orderViewStatus;
        public OrdersViewStatusType OrderViewStatus
        {
            get { return _orderViewStatus; }
            set
            {
                _orderViewStatus = value;
                LastOrderViewStatusUpdateTime = value == OrdersViewStatusType.New ? CreatingTime : DateTime.Now;
                NotifyPropertyChanged(nameof(OrderViewStatus));
            }
        }

        /// <summary>
        /// When OrderViewStatus was update last time
        /// Need to check that VM may to set next status after highlight animation completed
        /// </summary>
        public DateTime LastOrderViewStatusUpdateTime { get; set; }

        private readonly int _newStatusLimitSeconds;

        #endregion

        public OrderItemViewModel(Order order, int newStatusLimitSeconds)
        {
            _item = order;
            _newStatusLimitSeconds = newStatusLimitSeconds;
            OrderStatus = _item.OrderStatus;
        }

        public OrderItemViewModel(Order order)
            : this(order, 10)
        {}

        /// <summary>
        /// Convert VM to object
        /// </summary>
        /// <returns></returns>
        public Order ToModel()
        {
            return new Order(Id, Size, _item.OrderType, LimitPrice, StopPrice, ExpectedTradePrice, Tag, _item.ToProcessAs, OrderStatus);
        }
    }
}
