using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;

namespace ElEmegi.Ecommerce.Core.Model.Entity
{
   public class DataContext : DbContext
    {
        public DataContext() : base("dataConnection")
        {
            Database.SetInitializer(new DataInitializer());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<User> Users{ get; set; }
        public DbSet<Member> Members{ get; set; }
        public DbSet<Blog> Blogs{ get; set; }
        public DbSet<Log> Logs{ get; set; }
    }
}
