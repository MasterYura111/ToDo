using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security;

namespace ToDo.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "enter text")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "min lenght 3,max lenght 50 ")]
        public string Text { get; set; }
        public string UserId { get; set; }
        public virtual ICollection<List> Lists { get; set; }

        public Category()
        {
            Lists = new List<List>();
        }
       
    }
}