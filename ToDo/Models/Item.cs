using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToDo.Models
{
    public class Item
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "enter text")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "min lenght 3,max lenght 50 ")]
        public string Text { get; set; }
        public bool Checked { get; set; }

        public int ListId { get; set; }
        public virtual List List { get; set; }
    }
}