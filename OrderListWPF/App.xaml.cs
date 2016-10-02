using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Unity;
using OrderListWPF.GUI.DataProvider;
using OrderListWPF.GUI.ViewModel;
using OrderListWPF.GUI.ViewModel.Base;

namespace OrderListWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // initialize IoC container
            IUnityContainer container = new UnityContainer();

            #region Dependency Registration

            container.RegisterType<IOrderDataProvider, OrderDataProvider>();
            container.RegisterType<IOrderListViewModel, OrderListViewModel>();

            #endregion

            var mainWindow = container.Resolve<GUI.View.OrdersMainWindow>();
            mainWindow.Show();
        }
    }
}
