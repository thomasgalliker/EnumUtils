using System.ComponentModel;

namespace EnumUtils.Tests.Testdata
{
    public enum Weekday
    {
        Sun,
        Mon,
        Tue,
        Wed,
        Thu,

        [Description("Friday")]
        Fri,

        [Description("Saturday")]
        Sat
    };
}
