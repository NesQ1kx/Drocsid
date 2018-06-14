using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Drocsid.Models
{
    public class EditModel
    {
        [HiddenInput]
        public int Id { get; set; }

        //[Required]
        [Display(Name = "Изменить имя")]
        public string Username { get; set; }

        //[Required]
        [DataType(DataType.Password)]
        [Display(Name = "Изменить пароль")]
        public string Password { get; set; }
    }
}