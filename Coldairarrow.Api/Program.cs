using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace Coldairarrow.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            ////TryLoadAssembly();

            return Host.CreateDefaultBuilder(args)
                   .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                   .ConfigureWebHostDefaults(webBuilder =>
                   {
                       webBuilder
                           .UseUrls("http://*:5000")
                           .UseStartup<Startup>();
                   });
        }
    }
}
