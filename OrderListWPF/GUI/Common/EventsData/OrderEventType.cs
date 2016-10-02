namespace OrderListWPF.GUI.Common.EventsData
{
    /// <summary>
    /// OrderEventType
    /// </summary>
    public enum OrderEventType
    {
        AddNew,   // get new item from service
        Change,   // get changes in existing item
        Complete  // get completed order from service
    }
}
