using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApiApp.Models
{
    public class AuthModel
    {
        [Required(ErrorMessage = "username is required")]
        public string username { get; set; }
        [Required(ErrorMessage = "password is required")]
        public string password { get; set; }
    }
}