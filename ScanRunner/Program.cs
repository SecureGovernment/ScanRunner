using System;
using System.IO;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SecureGovernment.Data;
using SecureGovernment.Data.Infastructure;
using SecureGovernment.Domain;
using SecureGovernment.Domain.Interfaces.Infastructure;

namespace ScanRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configBuilder = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("appsettings.json", false, true)
                              .AddJsonFile($"appsettings.{(string.IsNullOrEmpty(environmentName) ? "Development" : environmentName)}.json", false, true);

            var configuration = configBuilder.Build();
            serviceCollection.Configure<Settings>(configuration);

            var builder = new ContainerBuilder();

            builder.Populate(serviceCollection);
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly(), Assembly.Load("SecureGovernment.Domain"), Assembly.Load("SecureGovernment.Data"))
                   .AsImplementedInterfaces().PropertiesAutowired();
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>();
            builder.Register(x => x.Resolve<IOptionsSnapshot<Settings>>().Value).As<ISettings>().InstancePerLifetimeScope();
            builder.RegisterType<ObservatoryContext>().PropertiesAutowired();

            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IApplication>();
                app.Run(args);
            }
        }
    }
}
