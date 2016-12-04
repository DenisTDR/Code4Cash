using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Code4Cash.Misc.Attributes
{
    public class UpdatableAttribute:Attribute
    {
        public bool Updatable { get; set; }
        public UpdatableAttribute(bool updatable = true)
        {
            this.Updatable = updatable;
        }
    }
}