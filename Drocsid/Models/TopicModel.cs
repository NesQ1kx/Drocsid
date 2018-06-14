using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Drocsid.Models
{
    public class TopicModel
    {
        [Required]
        [Display(Name = "Название темы")]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
        public string TopicName { get; set; }

        [Required]
        [Display(Name = "Содержание")]
        [StringLength(500, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
        public string Text { get; set; }

    }
}