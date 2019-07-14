using AutoMapper;
using McMaster.Extensions.CommandLineUtils;
using SecureGovernment.Data.Infastructure;
using SecureGovernment.Domain.Interfaces.Facades;
using SecureGovernment.Domain.Interfaces.Infastructure;
using Serilog;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ScanRunner
{
    public interface IApplication
    {
        void Run(string[] args);
    }

    public class Application : IApplication
    {
        public ISettings Settings { get; set; }
        public IWebsiteFacade WebsiteFacade { get; set; }
        public IScanFacade ScanFacade { get; set; }

        public void Run(string[] args)
        {
            var logDirectory = string.IsNullOrWhiteSpace(Settings.LogDirectory) ? Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) : Settings.LogDirectory;
            Log.Logger = new LoggerConfiguration().MinimumLevel.Error().WriteTo.RollingFile(Path.Combine(logDirectory, "log-{Date}.txt")).CreateLogger();
            Mapper.Initialize(config => config.AddProfile<MapperProfile>());

            var app = new CommandLineApplication()
            {
                Name = "scanrunner",
                FullName = "SecureGovernment Scan Runner",
            };

            app.HelpOption("-?|-h|--help");

            app.Command("version", (command) => {
                command.Description = "Prints the version";

                command.OnExecute(() =>
                {
                    Console.WriteLine(Settings.Version);
                });
            });

            app.Command("import", (command) => {
                command.Description = "Prints the version";
                command.Argument("File Path", "Path to the file to import.");

                command.OnExecute(() =>
                {
                    var filePath = command.Arguments.Single(x => x.Name == "File Path").Value;
                    var rawCsv = File.ReadAllText(filePath);
                    WebsiteFacade.AddWebsitesFromCsv(rawCsv);
                });
            });

            app.Command("scan", (command) => {
                command.Description = "Run next scan";

                command.OnExecute(() =>
                {
                    ScanFacade.Scan();
                });
            });

            app.OnExecute(() => {
                app.ShowHelp();
            });

            try
            {
                app.Execute(args);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(ex.Message);
                Log.Error(ex, string.Empty);
                Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
