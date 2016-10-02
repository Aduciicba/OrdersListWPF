namespace OrderListWPF.GUI.ViewModel
{
    /// <summary>
    /// Order state on view
    /// </summary>
    public enum OrdersViewStatusType
    {
        New = -1,       // active order with creation time <= new order time limit
        Active = 0,     // active order with creation time > new order time limit
        Compliting = 1, // complete order, which need to highlight
        Complete = 2    // complete order after highlighting finished
    }
}