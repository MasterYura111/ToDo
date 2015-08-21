using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ToDo.Models
{
    public class ToDoContext : DbContext
    {
        public ToDoContext()
            : base("DefaultConnection")
        { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<List> Lists { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<List>()
    .HasMany(l => l.Categories)
    .WithMany(c => c.Lists)
    .Map(m =>
    {
        m.ToTable("CategoryList");
        m.MapLeftKey("ListId");
        m.MapRightKey("CategoryId");
    });

            base.OnModelCreating(modelBuilder);
        }
    }


}