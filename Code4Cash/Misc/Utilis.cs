using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web;
using Code4Cash.Data.Models.Entities.Base;
using Code4Cash.Data.Models.ViewModels.Base;
using Code4Cash.Misc.Attributes;

namespace Code4Cash.Misc
{
    public static class Utilis
    {
        public static bool IsValidSelector(string selector)
        {
            Guid tmp;
            return Guid.TryParse(selector, out tmp);
        }
    }
}