using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDo.Models;

namespace ToDo.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        ToDoContext db = new ToDoContext();
        [HttpPost]
        public bool Change(int id, bool value)
        {
            Item item = db.Items.Find(id);
            if (item == null)
                return false;

            item.Checked = value;
            db.SaveChanges();
            return true;
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            List<Item> items = db.Items.Where(i => i.ListId == id).ToList();
            ViewBag.ListId = id;
            ViewBag.List = db.Lists.Find(id);
            return View(items);
        }

        [HttpPost]
        public ActionResult Edit(List<Item> item, int listId)
        {
            if (item == null)
                return HttpNotFound("not have eny item");

            for (int i = 0; i < item.Count; i++)
            {
                //item[i].
                if (item[i].Text == null && item[i].Id != 0)
                {
                    Item dellItemItem = db.Items.Find(item[i].Id);

                    db.Items.Remove(dellItemItem);
                    db.SaveChanges();
                }
                else if (item[i].Id != 0)
                {
                    Item editItem = db.Items.Find(item[i].Id);
                    if (editItem != null)
                    {
                        editItem.Text = item[i].Text;
                        db.Entry(editItem).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                        return HttpNotFound("text lenght min 3 , max 50");

                }
                else if (item[i].Id == 0)
                {
                    item[i].ListId = listId;
                    if (ModelState.IsValid)
                    {
                        db.Items.Add(item[i]);
                        db.SaveChanges();
                    }
                    else
                    {
                        return HttpNotFound("text lenght min 3 , max 50");
                    }

                }
            }

            return Redirect("/List/DetailList/" + listId);

        }
    }
}