using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecureGovernment.Data.Infastructure;
using SecureGovernment.Domain;
using System;

namespace SecureGovernment.Data.Tests
{
    [TestClass]
    public class BaseTest
    {
        public BaseTest()
        {
            if(Database == null)
            {
                var connectionString = "Host=127.0.0.1;Database=observatory;Username=postgres;Password=password";
                string environmentConnectionString = Environment.GetEnvironmentVariable("SECUREGOV_TEST_DATABASE");
                string hostName = Environment.GetEnvironmentVariable("SECUREGOV_TEST_DATABASE_HOSTNAME");

                if (!string.IsNullOrEmpty(environmentConnectionString))
                    connectionString = environmentConnectionString;
                else if (!string.IsNullOrEmpty(hostName))
                {
                    string database = Environment.GetEnvironmentVariable("SECUREGOV_TEST_DATABASE_NAME");
                    string username = Environment.GetEnvironmentVariable("SECUREGOV_TEST_DATABASE_USERNAME");
                    string password = Environment.GetEnvironmentVariable("SECUREGOV_TEST_DATABASE_PASSWORD");
                    connectionString = $"Host={hostName};Database={database};Username={username};Password={password}";
                }

                Database = new ObservatoryContext() { Settings = new Settings() {
                    ConnectionString = connectionString
                } };
            }
        }

        protected ObservatoryContext Database { get; set; }

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext testContext)
        {
            Mapper.Initialize(x => x.AddProfile<MapperProfile>());
            Mapper.AssertConfigurationIsValid();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Database.Database.ExecuteSqlCommand("DELETE FROM public.scans");
            Database.Database.ExecuteSqlCommand("DELETE FROM public.websites");
            Database.Database.ExecuteSqlCommand("DELETE FROM public.website_categories");
            Database.Database.ExecuteSqlCommand("DELETE FROM public.analysis");
            Database.Database.ExecuteSqlCommand("DELETE FROM public.trust");
            Database.Database.ExecuteSqlCommand("DELETE FROM public.certificates");
            Database.SaveChanges();
        }
    }
}
