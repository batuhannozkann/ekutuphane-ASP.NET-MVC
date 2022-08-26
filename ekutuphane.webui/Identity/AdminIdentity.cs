using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace ekutuphane.webui.Identity
{
    public static class AdminIdentity
    {
        public static async Task Seed(UserManager<User> userManager,RoleManager<IdentityRole> roleManager,IConfiguration configuration)
        {
            var username=configuration["Data:Admin:UserName"];
            var email=configuration["Data:Admin:Email"];
            var role = configuration["Data:Admin:Role"];
            var password = configuration["Data:Admin:Password"];

            if(await userManager.FindByNameAsync(username)==null)
            {
                await roleManager.CreateAsync(new IdentityRole(role));
                    var user = new User{
                        UserName=username,
                        Email=email,
                        EmailConfirmed=true,
                        FirstName="Admin",
                        LastName="Admin"
                    };
                        var result = await userManager.CreateAsync(user,password);
                        if(result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(user,role);
                        }
            }
        }
    }
}