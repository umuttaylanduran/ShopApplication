using Castle.Core.Configuration;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
namespace ShopApplication.UI.Identity
{
    public static class SeedIdentity
    {


        public static async Task Seed(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            //// appsettings.json dan çektiğimiz veriler.
            //var username = configuration["Data:AdminUser:username"];
            //var email = configuration["Data:AdminUser:email"];
            //var password = configuration["Data:AdminUser:password"];
            //var role = configuration["Data:AdminUser:role"];

            //if (await userManager.FindByNameAsync(username) == null)
            //{
            //    await roleManager.CreateAsync(new IdentityRole(role));

            //    var user = new ApplicationUser()
            //    {
            //        UserName = username,
            //        Email = email,
            //        FullName = "Admin User",
            //        //EmailConfirmed = true // gerek yok confirmed çalışmıyor.
            //    };

            //    var result = await userManager.CreateAsync(user, password);
            //    if (result.Succeeded)
            //    {
            //        await userManager.AddToRoleAsync(user, role);
            //    }
            //}
        }
    }
}
