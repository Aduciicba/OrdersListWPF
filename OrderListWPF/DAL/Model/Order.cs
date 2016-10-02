using System;
using OrderListWPF.DAL.Enum;
using OrderListWPF.DAL.Interface;

namespace OrderListWPF.DAL.Model
{
    /// <summary>
    /// Order
    /// </summary>
    public class Order : IOrder
    {
        public Order(
                      decimal size, 
                      OrderType type, 
                      double limitPrice, 
                      double stopPrice, 
                      double expectedTradePrice, 
                      string tag, 
                      OrderToProcessAs toProcessAs, 
                      OrderStatusType orderStatus)
            
        {
            Size = size;
            OrderType = type;
            LimitPrice = limitPrice;
            StopPrice = stopPrice;
            ExpectedTradePrice = expectedTradePrice;
            Tag = tag;
            ToProcessAs = toProcessAs;
            OrderStatus = orderStatus;
        }

        /// <summary>
        /// Additional ctor for creating object with explicity Id
        /// </summary>
        public Order(
                      int id,
                      decimal size,
                      OrderType type,
                      double limitPrice,
                      double stopPrice,
                      double expectedTradePrice,
                      string tag,
                      OrderToProcessAs toProcessAs,
                      OrderStatusType orderStatus)
            : this(size, type, limitPrice, stopPrice, expectedTradePrice, tag, toProcessAs, orderStatus)
        {
            Id = id;
            CreatingTime = DateTime.Now;
        }

        public OrderAction Action { get { return Size >= 0 ? OrderAction.Buy : OrderAction.Sell; } }
        public decimal Quantity { get { return Size >= 0 ? Size : -Size; } }
        public decimal Size { get; protected set; }
        public OrderType OrderType { get; protected set; }
        public double LimitPrice { get; protected set; }
        public double StopPrice { get; protected set; }
        public double ExpectedTradePrice { get; protected set; }
        public string Tag { get; protected set; }

        /// <summary>
        /// This members are here, not in OrderItemViewModel, because they sets on the DAL, i think :)
        /// </summary>
        public int Id { get; protected set; }
        public OrderStatusType OrderStatus { get; set; }
        public DateTime CreatingTime { get; protected set; }

        public OrderToProcessAs ToProcessAs { get; protected set; }

        public override string ToString()
        {
            return
                string
                    .Format("{5}-{0}{1}@{2}{3}{4}",
                        Size >= 0 ? "+" : "-", Quantity,
                        OrderType == OrderType.Limit
                            ? (LimitPrice + " LMT")
                            : (OrderType == OrderType.Stop
                                ? (StopPrice + " STP")
                                : (ExpectedTradePrice + "MKT")),
                        !string.IsNullOrEmpty(Tag) ? (" (" + Tag + ")") : "",
                        ToProcessAs != OrderToProcessAs.RealOrder ? ToProcessAs.ToString() : "",
                        Id);
        }

        public object Clone()
        {
            return new Order(Id, Size, OrderType, LimitPrice, StopPrice, ExpectedTradePrice, Tag, ToProcessAs, OrderStatus);
        }
    }

}
