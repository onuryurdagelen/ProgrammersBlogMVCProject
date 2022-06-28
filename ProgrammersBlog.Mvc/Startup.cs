using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProgrammersBlog.Services.AutoMapper.Profiles;
using ProgrammersBlog.Services.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProgrammersBlog.Mvc
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
            services.AddControllersWithViews().AddRazorRuntimeCompilation()
                .AddJsonOptions(opt =>
            {
                //opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });
            services.AddControllers()
                .AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddAutoMapper(typeof(CategoryProfile),typeof(ArticleProfile));
            services.AddSession(); //Session yapýsýný kullanmak için yapýlýr.
            services.AddRazorPages();
            services.LoadMyService(); //Service katmanýndaki Extension bölümünde ServiceCollectionExtensions class'ýnda alýrýz.
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Admin/User/Login"); //Login olan kullanýcýyý yönlendirmek için kullanýlýr.
                options.LogoutPath = new PathString("/Admin/User/Logout"); //Logout olan kullanýcýlarý yönlendirmek için kullanýlýr.
                options.Cookie = new CookieBuilder
                {
                    Name = "ProgrammersBlog",
                    HttpOnly = true, //Sadece server-side tarafýnda çalýþmayý saðlar.Client-side'ta gözükmez.Güvenlik için önemlidir.
                    SameSite = SameSiteMode.Strict, //Cookie bilgilerinin sadece kendi sitemizden geldiðinde kabul edilir.
                    SecurePolicy = CookieSecurePolicy.SameAsRequest //CookieSecurePolicy.Always doðru olandýr.
                };
                options.SlidingExpiration = true; //Kullanýcý siteye girdiðinde süre tanýr.
                options.ExpireTimeSpan = TimeSpan.FromDays(7); //7 gün boyunca bir daha giriþ yapmasý gerekmeyecek.Tarayýcý üzerinde cookie var olacak.
                options.AccessDeniedPath = new PathString("/Admin/User/AccessDenied"); //Kullanýcý giriþ yaptýðýnda yetkisi olmayan yere giriþ yaptýðýnda yönlendirilecek Path'tir.
            });



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
                app.UseExceptionHandler("/Error");
            }

            app.UseSession();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "Admin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
                    );
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
