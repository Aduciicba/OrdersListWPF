using System.Collections.ObjectModel;

namespace OrderListWPF.GUI.ViewModel.Base
{
    /// <summary>
    /// IOrderListViewModel
    /// </summary>
    public interface IOrderListViewModel
    {
        ObservableCollection<OrderItemViewModel> OrdersList { get; }
        OrderItemViewModel SelectedOrder { get; }

    }
}
