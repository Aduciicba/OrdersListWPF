using System.Collections.Generic;
using OrderListWPF.DAL.Model;
using OrderListWPF.GUI.Common.EventsData;

namespace OrderListWPF.GUI.DataProvider
{
    /// <summary>
    /// IOrderDataProvider - get/set order's data from source 
    /// </summary>
    public interface IOrderDataProvider
    {
        event OrderEvent OrdersChanged;
        IEnumerable<Order> GetActiveOrders(int count = 0);

        void ExecuteOrder(Order executedOrder);
        void GenerateOrder();

    }
}
