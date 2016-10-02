using System;
using OrderListWPF.DAL.Enum;
using OrderListWPF.DAL.Model;

namespace OrderListWPF.GUI.ViewModel.Base
{
    /// <summary>
    /// IOrderItemViewModel
    /// </summary>
    public interface IOrderItemViewModel
    {
        int Id { get; }
        string ActionName { get; }
        decimal Quantity { get; }
        decimal Size { get; }
        string OrderTypeName { get; }
        double LimitPrice { get; }
        double StopPrice { get; }
        double ExpectedTradePrice { get; }
        string Tag { get; }
        string ToProcessAsName { get; }
        DateTime CreatingTime { get; }
        OrderStatusType OrderStatus { get; }
        OrdersViewStatusType OrderViewStatus { get; }
        DateTime LastOrderViewStatusUpdateTime { get; }
        Order ToModel();
    }
}
