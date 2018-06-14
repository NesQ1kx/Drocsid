using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Drocsid.Models
{
    public class CommentModel
    {
        [Required]
        [Display(Name = "Ваш комментарий")]
        [StringLength(500, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 3)]
        public string Text { get; set; }
    }
}