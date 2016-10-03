using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using OrderListWPF.DAL.Enum;
using OrderListWPF.GUI.Common.EventsData;
using OrderListWPF.GUI.Common.Command;
using OrderListWPF.GUI.DataProvider;
using OrderListWPF.GUI.ViewModel.Base;

namespace OrderListWPF.GUI.ViewModel
{
    public class OrderListViewModel : BaseNotification, IOrderListViewModel
    {
        #region Data Providers

        private readonly IOrderDataProvider _ordersProvider;

        #endregion

        #region Members

        #region IOrderListViewModel implementation

        /// <summary>
        /// Source orders collection
        /// </summary>
        private ObservableCollection<IOrderItemViewModel> _ordersList;
        public ObservableCollection<IOrderItemViewModel> OrdersList
        {
            get
            {
                if (_ordersList == null)
                    _ordersList = new ObservableCollection<IOrderItemViewModel>();
                return _ordersList;
            }
            private set { _ordersList = value; }
        }

        /// <summary>
        /// Current selected order
        /// </summary>
        private IOrderItemViewModel _selectedOrder;
        public IOrderItemViewModel SelectedOrder
        {
            get { return _selectedOrder; }
            set
            {
                _selectedOrder = value;
                NotifyPropertyChanged(nameof(SelectedOrder));
            }
        }
        #endregion


        
        private int _ordersShowCount = 10;
        /// <summary>
        /// Number of orders to show in the datagrid
        /// </summary>
        public int OrdersShowCount
        {
            get { return _ordersShowCount; }
            set
            {
                if (_ordersShowCount == value)
                    return;
                _ordersShowCount = value;
                RefreshOrdersList();
            }
        }

        /// <summary>
        /// Item's highlighting time
        /// </summary>
        private int _highlightTimeSeconds = 10;

        /*
        /// <summary>
        /// New and completed orders highlight duration in seconds
        /// </summary>
        public int HighlightTimeSeconds
        {
            get { return _highlightTimeSeconds; }
            set
            {
                if (_highlightTimeSeconds == value)
                    return;
                _highlightTimeSeconds = value;
            }
        }*/

        #endregion

        #region Ctor & inizialization
        public OrderListViewModel(IOrderDataProvider ordersProvider)
        {
            _ordersProvider = ordersProvider;
            Initialize();
        }

        private void Initialize()
        {
            _ordersProvider.OrdersChanged += OrdersChanged;
            RefreshOrdersList();
        }

        #endregion

        /// <summary>
        /// Provider's OrdersChanged event handler
        /// Handle add new and complete existing orders
        /// </summary>
        private void OrdersChanged(object sender, OrderEventArgs e)
        {
            switch (e.EventType)
            {
                case OrderEventType.AddNew:
                    var item = new OrderItemViewModel(e.Order);
                    OrdersList.Insert(0, item);
                    RefreshOrdersList();
                    break;
                case OrderEventType.Change:
                    break;
                case OrderEventType.Complete:
                    var orderItem = OrdersList.FirstOrDefault(o => o.Id == e.Order.Id);
                    if (orderItem != null)
                    {
                        orderItem.OrderStatus = OrderStatusType.Complete;
                    }
                    break;
            }
        }

        #region Commands

        /// <summary>
        /// Complete selected order
        /// </summary>
        public ICommand ExecuteOrderCommand => new DelegateCommand(ExecuteOrder, () => SelectedOrder != null);

        /// <summary>
        /// Generate new order
        /// </summary>
        public ICommand GenerateOrderCommand => new DelegateCommand(GenerateOrder);

        /// <summary>
        /// Call after completed orders highlighting finished
        /// </summary>
        public ICommand SetOrdersCompleteCommand => new DelegateCommand(SetCopmplitingOrdersToComplete);

        /// <summary>
        /// Call after new orders highlighting finished
        /// </summary>
        public ICommand SetOrdersActiveCommand => new DelegateCommand(SetNewOrdersToActive);

        #endregion

        #region Commands methods

        /// <summary>
        /// Complete selected order
        /// </summary>
        private void ExecuteOrder()
        {
            if (SelectedOrder.OrderStatus == OrderStatusType.Complete)
                return;
            
            _ordersProvider.ExecuteOrder(SelectedOrder.ToModel());
        }

        /// <summary>
        /// Change new orders view status to active if highlighting time is expired
        /// </summary>
        private void SetNewOrdersToActive()
        {
            ChangeOrdersViewStatus(OrdersViewStatusType.New, OrdersViewStatusType.Active);
        }


        /// <summary>
        /// Change compliting orders view status to complete if highlighting time is expired
        /// and remove complete orders from source list
        /// </summary>
        private void SetCopmplitingOrdersToComplete()
        {
            ChangeOrdersViewStatus(OrdersViewStatusType.Compliting, OrdersViewStatusType.Complete);
            RemoveCompleteOrders();
            RefreshOrdersList();
        }

        /// <summary>
        /// Generate new order
        /// </summary>
        private void GenerateOrder()
        {
            _ordersProvider.GenerateOrder();
        }

        #endregion

        /// <summary>
        /// Remove complete orders from source list
        /// </summary>
        private void RemoveCompleteOrders()
        {
            if (OrdersList.Count(o => o.OrderViewStatus == OrdersViewStatusType.Complete) == 0)
                return;

            var removeList = OrdersList.Where(o => o.OrderViewStatus == OrdersViewStatusType.Complete)
                                       .ToList();

            foreach (var order in removeList)
            {
                OrdersList.Remove(order);
            }
        }

        /// <summary>
        /// Change order's OrderViewStatus from one to another
        /// </summary>
        /// <param name="from"></param>
        private void ChangeOrdersViewStatus(OrdersViewStatusType from, OrdersViewStatusType to)
        {
            var changeList = OrdersList.Where(o => o.OrderViewStatus == from
                                                && (DateTime.Now - o.LastOrderViewStatusUpdateTime).Seconds >= _highlightTimeSeconds).ToList();

            foreach (var order in changeList)
            {
                order.OrderViewStatus = to;
            }
        }

        /// <summary>
        /// Add or remove items from source OrdersList according to OrdersShowCount param
        /// </summary>
        private void RefreshOrdersList()
        {
            if (OrdersShowCount < OrdersList.Count)
            {
                while (OrdersShowCount < OrdersList.Count)
                    OrdersList.Remove(OrdersList.Last());
            }
            else
            {
                var ord = _ordersProvider.GetActiveOrders(OrdersShowCount).ToList();
                var list = ord.Where(o => OrdersList.Count(x => x.Id == o.Id) == 0)
                              .Select(o => new OrderItemViewModel(o)).ToList();
                foreach (var order in list)
                {
                    OrdersList.Add(order);
                }
            }
            NotifyPropertyChanged(nameof(OrdersList));
        }
    }
}
