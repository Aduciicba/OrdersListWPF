using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using OrderListWPF.GUI.ViewModel.Base;

namespace OrderListWPF.GUI.View
{
    /// <summary>
    /// Interaction logic for OrdersListView.xaml
    /// </summary>
    public partial class OrderListView : UserControl
    {
        #region Dependency Properties

        /// <summary>
        /// IOrderItemViewModel source item collection for binding to datagrid
        /// </summary>
        public static readonly DependencyProperty ItemsProperty =
        DependencyProperty.Register(
            "ItemsSource",
            typeof(IEnumerable<IOrderItemViewModel>),
            typeof(OrderListView),
            new UIPropertyMetadata(null));
        public IEnumerable<IOrderItemViewModel> ItemsSource
        {
            get { return (IEnumerable<IOrderItemViewModel>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        /// <summary>
        /// Current selected item
        /// </summary>
        public static readonly DependencyProperty SelectedItemProperty =
        DependencyProperty.Register(
            "SelectedItem",
            typeof(IOrderItemViewModel),
            typeof(OrderListView),
            new UIPropertyMetadata(null));
        public IOrderItemViewModel SelectedItem
        {
            get { return (IOrderItemViewModel)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        /// <summary>
        /// Command to handle animation of complited order
        /// </summary>
        public static readonly DependencyProperty OrderCompletingHandlerProperty =
        DependencyProperty.Register(
            "OrderCompletingHandler",
            typeof(ICommand),
            typeof(OrderListView),
            new UIPropertyMetadata(null));
        public ICommand OrderCompletingHandler
        {
            get { return (ICommand)GetValue(OrderCompletingHandlerProperty); }
            set { SetValue(OrderCompletingHandlerProperty, value); }
        }

        /// <summary>
        /// Command to handle animation of new order
        /// </summary>
        public static readonly DependencyProperty OrderNewHandlerProperty =
        DependencyProperty.Register(
            "OrderNewHandler",
            typeof(ICommand),
            typeof(OrderListView),
            new UIPropertyMetadata(null));
        public ICommand OrderNewHandler
        {
            get { return (ICommand)GetValue(OrderNewHandlerProperty); }
            set { SetValue(OrderNewHandlerProperty, value); }
        }

        /// <summary>
        /// Highlighting animation duration
        /// </summary>
        public static readonly DependencyProperty HighlightDurationProperty =
        DependencyProperty.Register(
            "HighlightDuration",
            typeof(Duration),
            typeof(OrderListView),
            new UIPropertyMetadata(null));
        public Duration HighlightDuration
        {
            get {
                OrdersGrid.DataContext = HighlightDuration;
                return (Duration)GetValue(HighlightDurationProperty);
                
            }
            set { SetValue(HighlightDurationProperty, value); OrdersGrid.DataContext = HighlightDuration; }
        }

        #endregion

        public OrderListView()
        {
            InitializeComponent();
        }



        /// <summary>
        /// Event handler for NewOrderStoryboard Completed event
        /// </summary>
        public void OnCompleteAnimationFinished(object sender, object e)
        {
            OrderCompletingHandler?.Execute(sender);

        }

        /// <summary>
        /// Event handler for CompletedOrderStoryboard Completed event
        /// </summary>
        public void OnNewAnimationFinished(object sender, object e)
        {
            OrderNewHandler?.Execute(sender);

        }
    }
}
