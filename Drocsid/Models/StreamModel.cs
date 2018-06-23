using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Drocsid.Models
{
    public class StreamModel
    {
        [Required]
        [Display(Name = "Название канала")]
        public string ChanelName { get; set; }

        [Required]
        [Display(Name = "Ссылка на канал")]
        public string Link { get; set; }

        [Required]
        [Display(Name = "Категория")]
        public string Category { get; set; }
    }
}