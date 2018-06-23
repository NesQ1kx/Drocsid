using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Drocsid.Models
{
    public class SectionModel
    {
        [Required]
        [Display(Name = "Название раздела")]
        public string SectionName { get; set; }
    }
}