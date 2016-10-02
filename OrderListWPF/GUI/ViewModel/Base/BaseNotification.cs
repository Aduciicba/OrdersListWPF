using System.ComponentModel;

namespace OrderListWPF.GUI.ViewModel.Base
{
    /// <summary>
    /// Base ViewModel's class that implemented INotifyPropertyChanged interface
    /// </summary>
    public class BaseNotification : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
