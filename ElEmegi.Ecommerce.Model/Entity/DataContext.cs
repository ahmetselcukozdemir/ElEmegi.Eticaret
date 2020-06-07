using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using ElEmegi.Ecommerce.Model.Entity;

namespace ElEmegi.Ecommerce.Model.Entity
{
   public class DataContext : DbContext
    {
        public DataContext() : base("dataConnection")
        {
            Database.SetInitializer(new DataInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories{ get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<User> Users{ get; set; }
        public DbSet<Member> Members{ get; set; }
        public DbSet<Blog> Blogs{ get; set; }
        public DbSet<Log> Logs{ get; set; }
        public DbSet<DiscountCoupon> DiscountCoupons{ get; set; }
        public DbSet<UserAddress> UserAddress { get; set; }
    }
}
