using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using TDShop.Common;
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
            //CreateSlider(context);
            //CreatePage(context);
            CreateContactDetail(context);
        }
        private void CreateUser(TDShopDbContext context)
        {
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
        private void CreateFooter(TDShopDbContext context)
        {
            if (context.Footers.Count(x => x.ID == CommonConstants.DefaultFooterId) == 0)
            {

            }
        }
        private void CreateSlider(TDShopDbContext context)
        {
            if (context.Slides.Count() == 0)
            {
                List<Slide> listSlide = new List<Slide>()
                {
                    new Slide(){Name="Slide 1", DisplayOrder=1,Status=true,Url="#",Image="/Assets/client/images/bag.jpg",Content=@"<h2>FLAT 50% 0FF</h2>
                                <label>FOR ALL PURCHASE <b>VALUE</b></label>
                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et </ p >
                                < span class=""on-get"">GET NOW</span>" },
                    new Slide(){Name="Slide 2", DisplayOrder=2,Status=true,Url="#",Image="/Assets/client/images/bag1.jpg",Content=@"<h2>FLAT 50% 0FF</h2>
                                <label>FOR ALL PURCHASE <b>VALUE</b></label>
                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et </ p >
                                < span class=""on-get"">GET NOW</span>" }
                };

                context.Slides.AddRange(listSlide);
                context.SaveChanges();
            }
        }

        private void CreatePage(TDShopDbContext context)
        {
            if (context.Pages.Count() == 0)
            {
                var page = new Page()
                {
                    Name ="Gioi thieu",
                    Alias = "gioi-thieu",
                    Content = "What is Lorem Ipsum?Lorem Ipsum is simply dummy text of the printing and typesetting industry." +
                    "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book." +
                    " It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release" +
                    " of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.Why do we use it" +
                    " ?It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more " +
                    "- or - less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors " +
                    "now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' " +
                    "will uncover many web sites still in their infancy.Various versions have evolved over the years, sometimes by accident, sometimes on purpose(injected humour and the like).",
                    Status = true
                };
                context.Pages.Add(page);
                context.SaveChanges();
            }
        }
        private void CreateContactDetail(TDShopDbContext context)
        {
            if (context.ContactDetails.Count() == 0)
            {
                try
                {
                    var contactDetail = new TDShop.Model.Models.ContactDetail()
                    {
                        Name = "Gia đình tôi",
                        Address = "Gia đình Tôi",
                        Email = "nguyenhoajt@gmail.com",
                        Lat = 21.450621,
                        Lng = 105.864754,
                        Phone = "0916605970",
                        Website = "",
                        Other = "",
                        Status = true

                    };
                    context.ContactDetails.Add(contactDetail);
                    context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var eve in ex.EntityValidationErrors)
                    {
                        Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        }
                    }
                }

            }
        }
    }
}