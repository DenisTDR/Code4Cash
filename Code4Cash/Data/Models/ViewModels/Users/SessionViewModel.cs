﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Code4Cash.Data.Models.ViewModels.Base;

namespace Code4Cash.Data.Models.ViewModels.Users
{
    public class SessionViewModel : ViewModel
    {
        public DateTime Expiration { get; set; }
        public string Token { get; set; }
    }
}