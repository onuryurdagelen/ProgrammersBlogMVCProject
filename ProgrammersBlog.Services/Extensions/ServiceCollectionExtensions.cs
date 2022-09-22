using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProgrammersBlog.DataAccess.Abstract;
using ProgrammersBlog.DataAccess.Concrete;
using ProgrammersBlog.DataAccess.EntityFramework.Contexts;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Services.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgrammersBlog.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<ProgrammersBlogContext>();
            serviceCollection.AddIdentity<User, Role>(options =>
            {
                //User Password Options
                options.Password.RequireDigit = false; //Password'lerde rakam zorunluluğunu kontrol eder.
                options.Password.RequiredLength = 5; //Password'lerin uzunluğunu verir.
                options.Password.RequiredUniqueChars = 0; //Unique karakterlerin kaçtane olması gerektiğini kontrol eder.
                options.Password.RequireNonAlphanumeric = false; //Aktif edildiğinde !,@,^ gibi karakterlerin zorunlu kılıp kılmadığını kontrol eder.
                options.Password.RequireLowercase = false; //Küyük harflerin zorunlu kılıp kılmadığını kontrol eder.
                options.Password.RequireUppercase = false; //Büyük harflerin zorunlu kılıp kılmadığını kontrol eder.

                //User Username and Email Options
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 -._@+$";
            })
                .AddEntityFrameworkStores<ProgrammersBlogContext>();
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<ICategoryService, CategoryManager>();
            serviceCollection.AddScoped<IArticleService, ArticleManager>();
            

            return serviceCollection;
        }
    }
}
