using System.ComponentModel;

namespace OrderListWPF.DAL.Enum
{
    /// <summary>
    /// OrderType
    /// </summary>
    public enum OrderType
    {
        [Description("Market")]
        Market,
        [Description("Limit")]
        Limit,
        [Description("Stop")]
        Stop
    }

}
