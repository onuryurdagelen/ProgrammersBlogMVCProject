using Microsoft.Extensions.DependencyInjection;
using ProgrammersBlog.Shared.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Shared.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            //Heryerde kullanılan ortak service'lerin IoC'leri yazılır.
            //Cache,HttpAccessor,Stopwatch gibi
            //throw new NotImplementedException();
        }
    }
}
