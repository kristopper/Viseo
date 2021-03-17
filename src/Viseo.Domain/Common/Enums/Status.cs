using System.ComponentModel;

namespace Viseo.Domain.Common.Enums
{
    public enum Status
    {
        [Description("Active")]
        Active = 1,
        [Description("InActive")]
        InActive = 2,
        [Description("Deleted")]
        Deleted = 3
    }
}