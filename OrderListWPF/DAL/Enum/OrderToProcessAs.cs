using System.ComponentModel;

namespace OrderListWPF.DAL.Enum
{
    /// <summary>
    /// OrderToProcessAs
    /// </summary>
    public enum OrderToProcessAs
    {
        [Description("Real Order")]
        RealOrder,
        [Description("WhatIf Order")]
        WhatIfOrder
    }

}
