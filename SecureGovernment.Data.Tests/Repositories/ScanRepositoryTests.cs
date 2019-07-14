using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ScanRunner.Data.Entities;
using SecureGovernment.Data.Infastructure;
using SecureGovernment.Data.Repositories;
using SecureGovernment.Domain.Interfaces.Infastructure;
using System;

namespace SecureGovernment.Data.Tests.Repositories
{
    [TestClass]
    public class ScanRepositoryTests : BaseTest
    {
        [TestMethod]
        public void ScanRepository_AddWebsiteToScan()
        {
            //Arrange
            Database.Scans.Add(new Scans() { Id = 23, Target = "usa.gov", ScanError = "", ValidationError = "", AnalysisParams = "{}", ConnInfo = "{}" });
            Database.Websites.Add(new Websites() { Id = 122, Hostname = "usa.gov" });
            Database.SaveChanges();

            var unitOfWorkFactory = new Mock<IUnitOfWorkFactory>();
            unitOfWorkFactory.Setup(x => x.GetUnitOfWork()).Returns(new UnitOfWork(Database));
            var repository = new ScanRepository(new DatabaseFactory(Database)) { UnitOfWorkFactory = unitOfWorkFactory.Object };

            //Act
            repository.AddWebsiteToScan(23, 122);

            var scan = Database.Scans.Find(23);
            //Assert
            Assert.IsNotNull(scan);
            Assert.AreEqual(122, scan.WebsiteId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ScanRepository_AddWebsiteToScan_NoScanFound()
        {
            var repository = new ScanRepository(new DatabaseFactory(Database));

            //Act
            repository.AddWebsiteToScan(23, 122);
        }
    }
}
