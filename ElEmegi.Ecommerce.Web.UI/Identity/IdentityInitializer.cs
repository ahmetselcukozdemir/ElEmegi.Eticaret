using ElEmegi.Ecommerce.Web.UI.Identity;
using ElEmegi.Ecommerce.Core.Model.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ElEmegi.Ecommerce.Web.UI.Identity
{
    public class IdentityInitializer : CreateDatabaseIfNotExists<IdentityDataContext>
    {
        protected override void Seed(IdentityDataContext context)
        {
       
            //rolleri oluştur

            if (!context.Roles.Any(i => i.Name == "admin"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole() { Name = "admin", Description = "admin rolü" };
                manager.Create(role);
            }


            if (!context.Roles.Any(i => i.Name == "user"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole() { Name = "user", Description = "user rolü" };
                manager.Create(role);
            }

            if (!context.Roles.Any(i => i.Name == "ahmetselcuk"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser() { Name = "ahmet", Surname = "özdemir", UserName = "ahmetselcuk", Email = "ahmetselcukozdemir01@gmail.com" };

                manager.Create(user, "1234567");
                manager.AddToRole(user.Id, "user");
                manager.AddToRole(user.Id, "user");
            }

            if (!context.Roles.Any(i => i.Name == "musabkomur"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser() { Name = "musab", Surname = "kömür", UserName = "musabkomur", Email = "musabkomur55@gmail.com" };

                manager.Create(user, "1234567");
                manager.AddToRole(user.Id, "user");
            }
        }
    }
}