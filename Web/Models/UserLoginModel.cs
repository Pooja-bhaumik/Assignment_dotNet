using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Models
{
    public class UserLoginModel
    {
        [Display(Name = "User")]
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
