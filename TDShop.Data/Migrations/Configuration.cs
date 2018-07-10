using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity.Migrations;
using System.Linq;
using TDShop.Model.Models;

namespace TDShop.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<TDShop.Data.TDShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TDShopDbContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new TDShopDbContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new TDShopDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "nguyenhoajt",
                Email = "nguyenhoa@gmail.com",
                EmailConfirmed = true,
                BirthDay = DateTime.Now,
                Fullname = "Nguyen hoa",
                Address = "PhoCo"
            };

            manager.Create(user, "1234564$");

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            var adminUser = manager.FindByEmail("nguyenhoa@gmail.com");

            manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
        }
    }
}