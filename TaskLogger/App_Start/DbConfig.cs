
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TaskLogger.Data.Concrete;
using TaskLogger.Data.Models;

namespace TaskLogger.App_Start
{
    public class DbConfig
    {
        public async static Task RegisterAdmin()
        {
            using (var roles = new RoleRepository())
            {
                if (await roles.GetRoleByNameAsync("admin") == null)
                {
                    await roles.CreateAsync(new IdentityRole("admin"));
                }
                if (await roles.GetRoleByNameAsync("editor") == null)
                {
                    await roles.CreateAsync(new IdentityRole("editor"));
                }
                if (await roles.GetRoleByNameAsync("user") == null)
                {
                    await roles.CreateAsync(new IdentityRole("author"));
                }
            }

            using (var users = new UserRepository())
            {
                var user = await users.GetUserByNameAsync("admin");

                if (user == null)
                {
                    var adminUser = new User()
                    {
                        UserName = "admin",
                        Email = "milenppavlov@gmail.com",
                        DisplayName = "Administrator",
                        Age = 31                    
                    };                
                    await users.CreateAsync(adminUser, "Password123!");

                    await users.AddUserToRoleAsync(adminUser, "admin");
                }
            }
        }
    }
}
