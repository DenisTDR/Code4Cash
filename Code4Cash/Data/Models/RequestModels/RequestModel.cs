using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Code4Cash.Data.Models.ViewModels.Base;

namespace Code4Cash.Data.Models.RequestModels
{
    public class RequestModel<TVm> where TVm : ViewModel
    {
        public string Token { get; set; }
        public TVm Data { get; set; }
    }
}
