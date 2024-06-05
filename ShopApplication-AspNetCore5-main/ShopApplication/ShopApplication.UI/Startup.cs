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




            // Adding Identity i�in konfig�rasyon i�lemleri
            services.AddDbContext<ApplicationIdentityDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>() // User ve Role bilgileri hangi s�n�ftan al�nacak.
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>() // identity verileri nereden �ekilecek.
                .AddDefaultTokenProviders(); // Parola veya email reset i�lemlerinde kullan�c�ya benzersiz bir token g�nderilir.

            services.Configure<IdentityOptions>(options =>
            {
                // Ek olarak istedi�imiz �zellikleri buradan belirtiyoruz.

                // password
                options.Password.RequireDigit = false; // Kullan�c� parolas�nda say� kullanma �art�.
                options.Password.RequireUppercase = false; // parolada b�y�k harf kullanma �art�. 
                options.Password.RequiredLength = 6; // parola minimum 6 karakterden olu�mal�.

                options.Lockout.MaxFailedAccessAttempts = 5; // Kullan�c�n�n parolay� yanl�� girmesi i�in 5 hakk� var.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Parolay� yanl�� giren kullan�c� 5 dakika boyunca tekrar eri�im sa�layamaz. 
                options.Lockout.AllowedForNewUsers = true;

                // options.User.AllowedUserNameCharacters = "";
                options.User.RequireUniqueEmail = true; // ayn� mail adresiyle �yeli�i engeller.

                options.SignIn.RequireConfirmedEmail = false; // Kullan�c�n�n mail adresinden onay i�lemi yapmas� gerekir.
                options.SignIn.RequireConfirmedPhoneNumber = false; // Telefon onay� gerekmiyor. (��nk� false)
            });

            services.ConfigureApplicationCookie(options => // Cookie yap�land�rmas�
            {
                options.LoginPath = "/account/login";
                options.LogoutPath = "/account/logout";
                options.AccessDeniedPath = "/account/accessdenied";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // 1 Saat boyunca Cookie taray�c�m�zda a��k kalacak. (Siteyi kapat�p a�sak bile kullan�c� giri�ine ihtiya� olmayacak)
                options.SlidingExpiration = true;
                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true,
                    Name = ".ShopApplication.Security.Cookie",
                    SameSite = SameSiteMode.Strict // cross street attack engelleme
                };
            });




            // Dependency Injection ile ProductDal istendi�inde kullan�c�ya hangisi g�nderilsin? --> Bu k�s�m� kullanarak istedi�imiz an de�i�iklik yapabiliriz.
            //services.AddScoped<IProductDal, MemoryProductDal>(); // MemoryProductDal'daki veriler �a��r�l�r.

            services.AddScoped<IProductDal, EfCoreProductDal>(); // DataAccess Layer ba�lant�s�
            services.AddScoped<ICategoryDal, EfCoreCategoryDal>();
            services.AddScoped<ICartDal, EfCoreCartDal>();
            services.AddScoped<IOrderDal, EfCoreOrderDal>();

            services.AddScoped<IProductService, ProductManager>(); // Business Layer ba�lant�s�
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICartService, CartManager>();
            services.AddScoped<IOrderService, OrderManager>();


            // EmailConfirm i�in SendGrid konfig�rasyonu
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

                SeedDatabase.Seed(); // Bizim olu�turdu�umuz SeedDatabase s�n�f�. �ncele
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles(); // wwroot u d��ar� a�mak i�in bir middleware (kendisi otomatik yaz�l� geliyor)

            //app.CustomStaticFiles(); // Middleware klas�r�n�n i�inde kendi olu�turdu�umu bir Middleware (30)

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            SeedIdentity.Seed(userManager, roleManager, Configuration).Wait(); // Otomatik bir admin kullan�c�s� olu�uyor. (adminuser@naturalshop.com)

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

                endpoints.MapControllerRoute( // Category'ye g�re filtreleme i�lemi --> /products/meyveler, /products/sebzeler vb.
                    name: "products",
                    pattern: "products/{category?}",
                    defaults: new {controller = "Shop", action = "List"}
                    );
             
            });

            




        }
    }
}
