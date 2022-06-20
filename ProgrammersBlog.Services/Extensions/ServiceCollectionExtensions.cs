using Microsoft.Extensions.DependencyInjection;
using ProgrammersBlog.DataAccess.Abstract;
using ProgrammersBlog.DataAccess.Concrete;
using ProgrammersBlog.DataAccess.EntityFramework.Contexts;
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
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<ICategoryService, CategoryManager>();
            serviceCollection.AddScoped<IArticleService, ArticleManager>();

            return serviceCollection;
        }
    }
}
