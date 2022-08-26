using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ekutuphane.business.Abstract;
using ekutuphane.business.Concrete;
using ekutuphane.data.Abstract;
using ekutuphane.data.Concrete.EfCore;
using ekutuphane.webui.EmailService;
using ekutuphane.webui.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace ekutuphane.webui
{
    public class Startup
    {
        private IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration=configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<LibraryContext>(options=>options.UseSqlServer(_configuration.GetConnectionString("MsSqlHost")));
            services.AddDbContext<ApplicationContext>(options=>options.UseSqlServer(_configuration.GetConnectionString("MsSqlHost")));
            services.AddIdentity<User,IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();
            services.AddControllersWithViews();
            services.AddScoped<IBookRepository,EfCoreBookRepository>();
            services.AddScoped<ICategoryRepository,EfCoreCategoryRepository>();
            services.AddScoped<IUnitOfWork,UnitOfWork>();
            //services.AddScoped<IBookService,BookService>();
            //services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IEmailSender,SmtpEmailSender>(p=>new SmtpEmailSender(
                _configuration["EmailSender:Host"],
                _configuration.GetValue<int>("EmailSender:Port"),
                _configuration.GetValue<bool>("EmailSender:EnableSSL"),
                _configuration["EmailSender:UserName"],
                _configuration["EmailSender:password"]
            ));
            services.Configure<IdentityOptions>(options=>{
                options.Password.RequiredLength=6;
                options.Password.RequireUppercase=true;
                options.Password.RequireDigit=true;
                options.Password.RequireNonAlphanumeric=false;
                // Lockout
                options.Lockout.MaxFailedAccessAttempts=5;
                options.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromMinutes(5);
                options.Lockout.AllowedForNewUsers=true;
                //Username
                options.User.RequireUniqueEmail=true;
                options.SignIn.RequireConfirmedEmail=true;
            });
            services.ConfigureApplicationCookie(options=>{
                options.LoginPath="/Account/login";
                options.LogoutPath="/Account/Logout";
                options.AccessDeniedPath="/Account/AccesDenied";
                options.SlidingExpiration=true;
                options.ExpireTimeSpan=TimeSpan.FromMinutes(15);
                options.Cookie=new CookieBuilder{
                    HttpOnly=true,
                    Name=".Ekutuphane.Security.Cookie",
                    SameSite=SameSiteMode.Strict
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IConfiguration configuration,IApplicationBuilder app, IWebHostEnvironment env,RoleManager<IdentityRole> roleManager,UserManager<User> userManager)
        {
            app.UseStaticFiles();//for wwwroot file
            app.UseStaticFiles(new StaticFileOptions{
                FileProvider=new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(),"node_modules")),
                    RequestPath="/modules"
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name:"RoleEdit",
                    pattern:"Role/Edit/{id}",
                    defaults:new{controller="Admin",action="EditRole"}
                );

                endpoints.MapControllerRoute(
                    name:"DeleteBookinCategory",
                    pattern:"Delete/{BookId}/{CategoryId}/{categoryname}",
                    defaults:new{controller="Admin",action="DeleteBookinCategory"}
                    );
                endpoints.MapControllerRoute(
                    name:"EditCategory",
                    pattern:"Category/{categoryname}",
                    defaults:new{controller="Admin",action="EditCategory"}
                    );
                endpoints.MapControllerRoute(
                    name:"EditBook",
                    pattern:"EditBook/{id}",
                    defaults:new{controller="Admin",action="EditBook"}
                );
                endpoints.MapControllerRoute(
                    name:"list",
                    pattern:"Books/{kategori?}",
                    defaults:new{controller="Library",action="BookList"}
                );
                endpoints.MapControllerRoute(
                    name:"detail",
                    pattern:"Book/{id?}",
                    defaults:new{controller="Library",action="Detail"}
                );
                endpoints.MapControllerRoute(
                        name:"default",
                        pattern:"{controller=Library}/{action=BookList}/{id?}"
                );
            });
            AdminIdentity.Seed(userManager,roleManager,configuration).Wait();
        }
    }
}
