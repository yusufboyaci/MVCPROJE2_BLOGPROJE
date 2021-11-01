using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVCPROJE2_BLOGPROJE.DATAACCESS.Context;
using MVCPROJE2_BLOGPROJE.DATAACCESS.SeedMethods;
using MVCPROJE2_BLOGPROJE.SERVICES.EmailService.Concrete;
using MVCPROJE2_BLOGPROJE.SERVICES.FileService.Abstract;
using MVCPROJE2_BLOGPROJE.SERVICES.FileService.Concrete;
using MVCPROJE2_BLOGPROJE.SERVICES.RegistrationService.Abstract;
using MVCPROJE2_BLOGPROJE.SERVICES.RegistrationService.Concrete;
using MVCPROJE2_BLOGPROJE.SERVICES.Repositories.Abstract;
using MVCPROJE2_BLOGPROJE.SERVICES.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCPROJE2_BLOGPROJE.WEBUI
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
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddTransient<IRegistrationService, RegistrationService>();
            services.AddTransient<IUpdateRegistrationService, UpdateRegistrationService>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<IUyeRepository, UyeRepository>();
            services.AddTransient<IMakaleRepository, MakaleRepository>();
            services.AddTransient<IKonuRepository, KonuRepository>();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Server=.;database=MVCPROJE2_BLOGDB;uid=yusuf;pwd=123"));
            services.AddIdentity<IdentityUser, IdentityRole>(x =>
            {
                x.Password.RequireDigit = false;
                x.Password.RequireLowercase = false;
                x.Password.RequireUppercase = false;
                x.Password.RequireNonAlphanumeric = false;
                x.Password.RequiredLength = 3;
            }).AddEntityFrameworkStores<ApplicationDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath= "/Account/Logout";
                options.Cookie = new CookieBuilder
                {
                    Name = "MVCPROJE2BLOGPROJE",
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict,
                    SecurePolicy = CookieSecurePolicy.Always
                };
                options.AccessDeniedPath = new PathString("/Account/AccessDenied");
            });
           
            services.AddDistributedMemoryCache();
            services.AddSession();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseSession();
            app.UseStaticFiles();
            SeedData.Seed(app);            
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "AdminArea",
                    areaName: "AdminArea",
                    pattern: "AdminArea/{controller=Login}/{action=LoginForm}/{id?}"
                    );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
