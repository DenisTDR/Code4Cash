using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Code4Cash.Data.Models.RequestModels
{
    public class LoginRequestModel
    {
        [Required]
        [MinLength(3)]
        public string Username { get; set; }
        [Required]
        [MinLength(3)]
        public string Password { get; set; }
    }
}