using ElasticView.AppService.Contracts;
using ElasticView.AppService;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyModel;
using System.Reflection;
using System.Runtime.Loader;

namespace ElasticView.UI
{
    public class AppContainer
    {
        private static ServiceProvider serviceProvider;

        public static void RegisterServices()
        {
            var services = new ServiceCollection();
            //services.AddSingleton<MainWindow>();
            var assemblies = GetAssemblies();
            services.AddAutoMapper(assemblies);

            services.AddSingleton<IElasticAppService, ElasticAppService>();
            services.AddSingleton<IElasticIndexAppService, ElasticIndexAppService>();
            services.AddSingleton<IElasticCatAppService, ElasticCatAppService>();

            serviceProvider = services.BuildServiceProvider();
        }

        public static T Resolve<T>()
        {
            return serviceProvider.GetRequiredService<T>();
        }


        private static Assembly[] GetAssemblies()
        {
            return DependencyContext.Default.CompileLibraries
                .Where(x => !x.Serviceable && x.Type != "package")
                .Select(x => AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(x.Name))).ToArray();
        }
    }
}
