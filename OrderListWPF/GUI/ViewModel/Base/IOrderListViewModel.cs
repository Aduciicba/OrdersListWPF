using System.Collections.ObjectModel;

namespace OrderListWPF.GUI.ViewModel.Base
{
    /// <summary>
    /// IOrderListViewModel
    /// </summary>
    public interface IOrderListViewModel
    {
        ObservableCollection<IOrderItemViewModel> OrdersList { get; }
        IOrderItemViewModel SelectedOrder { get; }

    }
}
