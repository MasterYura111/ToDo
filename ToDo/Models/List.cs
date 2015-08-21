using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ToDo.Models
{
    public class List
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "enter text")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "min lenght 3,max lenght 50 ")]
        public string Text { get; set; }
        public string UserId { get; set; }
        public virtual ICollection<Category> Categories { get; set; }

        public virtual ICollection<Item> Items { get; set; }

        public List()
        {
            Categories = new List<Category>();
            Items=new List<Item>();
        }
    }
}