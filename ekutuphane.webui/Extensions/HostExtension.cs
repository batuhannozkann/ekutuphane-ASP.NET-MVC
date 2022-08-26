using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ekutuphane.data.Concrete.EfCore;
using ekutuphane.webui.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ekutuphane.webui.Extensions
{
    public static class HostExtension
    {
        public static IHost DatabaseMigrate(this IHost host)
        {
            using(var scope=host.Services.CreateScope())
            {
                using(var applicationContext=scope.ServiceProvider.GetRequiredService<ApplicationContext>())
                {
                    try
                    {
                        applicationContext.Database.Migrate();
                    }
                    catch (System.Exception)
                    {
                        throw;
                    }
                }
                using(var libraryContext=scope.ServiceProvider.GetRequiredService<LibraryContext>())
                {
                    try
                    {
                        libraryContext.Database.Migrate();
                    }
                    catch (System.Exception)
                    {
                        throw;
                    }
                }
            }
            return host;
        }
    }
}