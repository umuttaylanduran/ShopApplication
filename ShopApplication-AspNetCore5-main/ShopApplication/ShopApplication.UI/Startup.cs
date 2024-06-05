using Castle.Core.Smtp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopApplication.Business.Abstract;
using ShopApplication.Business.Concrete;
using ShopApplication.DataAccess.Abstract;
using ShopApplication.DataAccess.Concrete.EntityFramework;
using ShopApplication.UI.Identity;
using ShopApplication.UI.Middleware;
using ShopApplication.UI.Services.EmailServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApplication.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();




            // Adding Identity için konfigürasyon iþlemleri
            services.AddDbContext<ApplicationIdentityDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>() // User ve Role bilgileri hangi sýnýftan alýnacak.
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>() // identity verileri nereden çekilecek.
                .AddDefaultTokenProviders(); // Parola veya email reset iþlemlerinde kullanýcýya benzersiz bir token gönderilir.

            services.Configure<IdentityOptions>(options =>
            {
                // Ek olarak istediðimiz özellikleri buradan belirtiyoruz.

                // password
                options.Password.RequireDigit = false; // Kullanýcý parolasýnda sayý kullanma þartý.
                options.Password.RequireUppercase = false; // parolada büyük harf kullanma þartý. 
                options.Password.RequiredLength = 6; // parola minimum 6 karakterden oluþmalý.

                options.Lockout.MaxFailedAccessAttempts = 5; // Kullanýcýnýn parolayý yanlýþ girmesi için 5 hakký var.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Parolayý yanlýþ giren kullanýcý 5 dakika boyunca tekrar eriþim saðlayamaz. 
                options.Lockout.AllowedForNewUsers = true;

                // options.User.AllowedUserNameCharacters = "";
                options.User.RequireUniqueEmail = true; // ayný mail adresiyle üyeliði engeller.

                options.SignIn.RequireConfirmedEmail = false; // Kullanýcýnýn mail adresinden onay iþlemi yapmasý gerekir.
                options.SignIn.RequireConfirmedPhoneNumber = false; // Telefon onayý gerekmiyor. (Çünkü false)
            });

            services.ConfigureApplicationCookie(options => // Cookie yapýlandýrmasý
            {
                options.LoginPath = "/account/login";
                options.LogoutPath = "/account/logout";
                options.AccessDeniedPath = "/account/accessdenied";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // 1 Saat boyunca Cookie tarayýcýmýzda açýk kalacak. (Siteyi kapatýp açsak bile kullanýcý giriþine ihtiyaç olmayacak)
                options.SlidingExpiration = true;
                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true,
                    Name = ".ShopApplication.Security.Cookie",
                    SameSite = SameSiteMode.Strict // cross street attack engelleme
                };
            });




            // Dependency Injection ile ProductDal istendiðinde kullanýcýya hangisi gönderilsin? --> Bu kýsýmý kullanarak istediðimiz an deðiþiklik yapabiliriz.
            //services.AddScoped<IProductDal, MemoryProductDal>(); // MemoryProductDal'daki veriler çaðýrýlýr.

            services.AddScoped<IProductDal, EfCoreProductDal>(); // DataAccess Layer baðlantýsý
            services.AddScoped<ICategoryDal, EfCoreCategoryDal>();
            services.AddScoped<ICartDal, EfCoreCartDal>();
            services.AddScoped<IOrderDal, EfCoreOrderDal>();

            services.AddScoped<IProductService, ProductManager>(); // Business Layer baðlantýsý
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICartService, CartManager>();
            services.AddScoped<IOrderService, OrderManager>();


            // EmailConfirm için SendGrid konfigürasyonu
            //services.AddTransient<IEmailSender, EmailSender>();


            services.AddMvc();
            // IProduct, EfCoreProductDal
            // IProduct, MySqlProductDal
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) // user ve role manager eklemesi.
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                SeedDatabase.Seed(); // Bizim oluþturduðumuz SeedDatabase sýnýfý. Ýncele
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles(); // wwroot u dýþarý açmak için bir middleware (kendisi otomatik yazýlý geliyor)

            //app.CustomStaticFiles(); // Middleware klasörünün içinde kendi oluþturduðumu bir Middleware (30)

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            SeedIdentity.Seed(userManager, roleManager, Configuration).Wait(); // Otomatik bir admin kullanýcýsý oluþuyor. (adminuser@naturalshop.com)

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "adminProducts",
                pattern: "admin/products",
                defaults: new { controller = "Admin", action = "ProductList" }
              );

                endpoints.MapControllerRoute(
                    name: "cart",
                    pattern: "cart",
                    defaults: new { controller = "Cart", action = "Index" }
                );

                endpoints.MapControllerRoute(
                    name: "checkout",
                    pattern: "checkout",
                    defaults: new { controller = "Cart", action = "Checkout" }
                );

                endpoints.MapControllerRoute(
                    name: "adminProducts",
                    pattern: "admin/products/{id?}",
                    defaults: new { controller = "Admin", action = "EditProduct" }
                );

                endpoints.MapControllerRoute(
                    name: "orders",
                    pattern: "orders",
                    defaults: new { controller = "Cart", action = "GetOrders" }
                );

                endpoints.MapControllerRoute(
                name: "adminCategories",
                pattern: "admin/categories",
                defaults: new { controller = "Admin", action = "CategoryList" }
              );

                endpoints.MapControllerRoute(
                    name: "adminCategories",
                    pattern: "admin/editcategory/{id?}",
                    defaults: new { controller = "Admin", action = "EditCategory" }
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                        );

                endpoints.MapControllerRoute( // Category'ye göre filtreleme iþlemi --> /products/meyveler, /products/sebzeler vb.
                    name: "products",
                    pattern: "products/{category?}",
                    defaults: new {controller = "Shop", action = "List"}
                    );
             
            });

            




        }
    }
}
