using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ToDo.Models;

namespace ToDo.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        protected string CurrentUserId()
        {
            ApplicationUserManager userManager = HttpContext.GetOwinContext()
                                              .GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            return user.Id;
        }

        ToDoContext db = new ToDoContext();
        
        public ActionResult Categories()
        {
            string currentUserId = CurrentUserId();
            IEnumerable<Category> categories = db.Categories.Where(c => c.UserId == currentUserId);
            return View(categories);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            ApplicationUserManager userManager = HttpContext.GetOwinContext()
                                            .GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            category.UserId = user.Id;
            db.Categories.Add(category);
            db.SaveChanges();
            return RedirectToAction("Categories");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            string currentUserId = CurrentUserId();
            if (id == null)
                return HttpNotFound();
            Category category = new Category();
            category = db.Categories.FirstOrDefault(c => c.Id == id && c.UserId == currentUserId);
            if (category == null)
                return HttpNotFound();
            return View(category);
        }
        [HttpPost]
        public ActionResult Edit(Category category)
        {
            Category newCategory = new Category();
            newCategory = db.Categories.Find(category.Id);
            newCategory.Text = category.Text;

            db.Entry(newCategory).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Categories");
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            string currentUserId = CurrentUserId();
            Category Category = db.Categories.FirstOrDefault(c => c.Id == id && c.UserId == currentUserId);
            if (Category == null)
                return HttpNotFound();
            return View(Category);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            string currentUserId = CurrentUserId();
            Category category = db.Categories.FirstOrDefault(c => c.Id == id && c.UserId == currentUserId);
            if (category == null)
                return HttpNotFound();
            category.Lists.Clear();
            db.SaveChanges();


            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Categories");
        }
    }
}