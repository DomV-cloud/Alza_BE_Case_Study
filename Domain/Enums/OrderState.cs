using System.ComponentModel;

namespace Domain.Enums
{
    public enum OrderState
    {
        [Description("Nová")]
        New = 0,
        [Description("Zaplacená")]
        Paid = 1,
        [Description("Zrušená")]
        Canceled = 2
    }
}
