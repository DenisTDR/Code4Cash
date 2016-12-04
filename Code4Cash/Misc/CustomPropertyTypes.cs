using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Code4Cash.Misc
{
    [Flags]
    public enum CustomPropertyTypes
    {
        None = 0,
        Updatable = 1,
        Primitive = 2,
        Entity = 4,
        NonEntity = 8,
        Enumerable = 16
    }
}