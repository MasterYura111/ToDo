using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ToDo.Models;

namespace ToDo.Controllers
{
    [Authorize]
    public class ListController : Controller
    {
        ToDoContext db = new ToDoContext();
        protected string CurrentUserId()
        {
            ApplicationUserManager userManager = HttpContext.GetOwinContext()
                                              .GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            return user.Id;
        }

        [HttpGet]
        public ActionResult Lists()
        {
            string currentUserId = CurrentUserId();
            IEnumerable<List> lists = db.Lists.Where(l => l.UserId == currentUserId);
            return View(lists);
        }

        [HttpGet]
        public ActionResult DetailList(int? id)
        {
            string currentUserId = CurrentUserId();
            if (id == null)
                return HttpNotFound();

            List list = new List();
            list = db.Lists.FirstOrDefault(l => l.Id == id && l.UserId == currentUserId);
            if (list == null)
                return HttpNotFound();

            return View(list);
        }

        [HttpGet]
        public ActionResult Create()
        {
            string currentUserId = CurrentUserId();
            IEnumerable<Category> categories = db.Categories.Where(c => c.UserId == currentUserId); ;
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        public ActionResult Create(List list, int[] selectedCategories)
        {
            if (list == null)
                return HttpNotFound();

            ApplicationUserManager userManager = HttpContext.GetOwinContext()
                                            .GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            list.UserId = user.Id;
            if (selectedCategories != null)
            {
                foreach (var cat in db.Categories.Where(c => selectedCategories.Contains(c.Id)))
                {
                    list.Categories.Add(cat);
                }
            }

            if (!ModelState.IsValid)
                return HttpNotFound();

            db.Lists.Add(list);
            db.SaveChanges();

            return RedirectToAction("Lists");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            string currentUserId = CurrentUserId();
            if (id == null)
                return HttpNotFound();
            List list = new List();
            list = db.Lists.FirstOrDefault(l => l.Id == id && l.UserId == currentUserId);
            if (list == null)
                return HttpNotFound();

            IEnumerable<Category> categories = db.Categories.Where(c => c.UserId == currentUserId); ;
            ViewBag.Categories = categories;
            return View(list);
        }
        [HttpPost]
        public ActionResult Edit(List list, int[] selectedCategories)
        {
            List newList = new List();
            newList = db.Lists.Find(list.Id);
            newList.Text = list.Text;
            newList.Categories.Clear();
            if (selectedCategories != null)
            {
                foreach (var cat in db.Categories.Where(c => selectedCategories.Contains(c.Id)))
                {
                    newList.Categories.Add(cat);
                }
            }

            db.Entry(newList).State = EntityState.Modified;
            db.SaveChanges();
            return Redirect("/list/detailList/" + newList.Id);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            string currentUserId = CurrentUserId();
            List list = db.Lists.FirstOrDefault(l => l.Id == id && l.UserId == currentUserId);
            if (list == null)
                return HttpNotFound();
            return View(list);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            string currentUserId = CurrentUserId();
            List list = db.Lists.FirstOrDefault(l => l.Id == id && l.UserId == currentUserId);
            if (list == null)
                return HttpNotFound();
            list.Categories.Clear();
            db.SaveChanges();

            List<Item> items = db.Items.Where(i => i.ListId == id).ToList();
            for (int i = 0; i < items.Count; i++)
            {
                Item dellItemItem = db.Items.Find(items[i].Id);
                db.Items.Remove(dellItemItem);
                db.SaveChanges();
            }

            db.Lists.Remove(list);
            db.SaveChanges();
            return RedirectToAction("Lists");
        }

    }
}