using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using TDShop.Model.Models;

namespace TDShop.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<TDShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TDShopDbContext context)
        {
            CreateProductCategorySample(context);
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new TDShopDbContext()));

            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new TDShopDbContext()));

            //var user = new ApplicationUser()
            //{
            //    UserName = "nguyenhoajt",
            //    Email = "nguyenhoa@gmail.com",
            //    EmailConfirmed = true,
            //    BirthDay = DateTime.Now,
            //    Fullname = "Nguyen hoa",
            //    Address = "PhoCo"
            //};

            //manager.Create(user, "1234564$");

            //if (!roleManager.Roles.Any())
            //{
            //    roleManager.Create(new IdentityRole { Name = "Admin" });
            //    roleManager.Create(new IdentityRole { Name = "User" });
            //}

            //var adminUser = manager.FindByEmail("nguyenhoa@gmail.com");

            //manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
        }

        private void CreateProductCategorySample(TDShopDbContext context)
        {
            if(context.ProductCategories.Count()==0)
            {
                List<ProductCategory> listProductCategory = new List<ProductCategory>()
                {
                 new ProductCategory(){Name = "Điện lạnh", Alias="dien-lanh", Status=true},
                 new ProductCategory(){Name = "Viễn thông", Alias="vien-thong", Status=true},
                 new ProductCategory(){Name = "Đồ gia dụng", Alias="do-gia-dung", Status=true},
                 new ProductCategory(){Name = "Mĩ phẩm", Alias="mi-pham", Status=true},

                };
                context.ProductCategories.AddRange(listProductCategory);
                context.SaveChanges();
            }
            
        }
    }
}