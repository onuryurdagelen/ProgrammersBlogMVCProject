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
            services.AddSession(); //Session yap�s�n� kullanmak i�in yap�l�r.
            services.AddRazorPages();
            services.LoadMyService(); //Service katman�ndaki Extension b�l�m�nde ServiceCollectionExtensions class'�nda al�r�z.
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Admin/User/Login"); //Login olan kullan�c�y� y�nlendirmek i�in kullan�l�r.
                options.LogoutPath = new PathString("/Admin/User/Logout"); //Logout olan kullan�c�lar� y�nlendirmek i�in kullan�l�r.
                options.Cookie = new CookieBuilder
                {
                    Name = "ProgrammersBlog",
                    HttpOnly = true, //Sadece server-side taraf�nda �al��may� sa�lar.Client-side'ta g�z�kmez.G�venlik i�in �nemlidir.
                    SameSite = SameSiteMode.Strict, //Cookie bilgilerinin sadece kendi sitemizden geldi�inde kabul edilir.
                    SecurePolicy = CookieSecurePolicy.SameAsRequest //CookieSecurePolicy.Always do�ru oland�r.
                };
                options.SlidingExpiration = true; //Kullan�c� siteye girdi�inde s�re tan�r.
                options.ExpireTimeSpan = TimeSpan.FromDays(7); //7 g�n boyunca bir daha giri� yapmas� gerekmeyecek.Taray�c� �zerinde cookie var olacak.
                options.AccessDeniedPath = new PathString("/Admin/User/AccessDenied"); //Kullan�c� giri� yapt���nda yetkisi olmayan yere giri� yapt���nda y�nlendirilecek Path'tir.
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
