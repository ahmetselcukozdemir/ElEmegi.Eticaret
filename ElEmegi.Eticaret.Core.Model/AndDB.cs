using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using ElEmegi.Eticaret.Core.Model.Entity;

namespace ElEmegi.Eticaret.Core.Model
{
   public class AndDB : DbContext
    {
        //tablolar
        
        public AndDB()
        :base(@"Data Source=DESKTOP-EU8IQQD\ahmet;Initial Catalog=ElEmegiDB_Test;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=True")
        {
          
        }

        public DbSet<User> Users{ get; set; }
        public DbSet<UserAddress> UserAddresses{ get; set; }
        public DbSet<Category> Categories{ get; set; }
        public DbSet<Product> Products{ get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Basket> Baskets{ get; set; }
        public DbSet<Order> Orders{ get; set; }
        public DbSet<OrderProduct> OrderProducts{ get; set; }
        public DbSet<OrderPayment> OrderPayments{ get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
