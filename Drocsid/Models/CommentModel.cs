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
        public string Text { get; set; }
    }
}