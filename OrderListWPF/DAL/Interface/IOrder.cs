using System;
using OrderListWPF.DAL.Enum;

namespace OrderListWPF.DAL.Interface
{
    /// <summary>
    /// IOrder
    /// </summary>
    public interface IOrder : ICloneable
    {
        OrderAction Action { get; }
        decimal Size { get; }
        decimal Quantity { get; }
        OrderType OrderType { get; }
        double LimitPrice { get; }
        double StopPrice { get; }
        double ExpectedTradePrice { get; }
        OrderToProcessAs ToProcessAs { get; }
        string Tag { get; }

        string ToString();
    }

}
