using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApiApp.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "First name is required")]
        public string firstname { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        public string lastname { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public string gender { get; set; }
        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime date_of_birth { get; set; }
    }

    public class EditUserModel
    {
        [Required(ErrorMessage = "Id is required")]
        public int? id { get; set; }
        [Required(ErrorMessage = "First name is required")]
        public string firstname { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        public string lastname { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public string gender { get; set; }
        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime date_of_birth { get; set; }
    }
}