using System.Windows;
using Microsoft.Practices.Unity;
using OrderListWPF.GUI.ViewModel.Base;

namespace OrderListWPF.GUI.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class OrdersMainWindow : Window
    {
        [Dependency]
        public IOrderListViewModel ViewModel
        {
            set { DataContext = value; }
        }

        public OrdersMainWindow()
        {
            InitializeComponent();
        }
    }
}
