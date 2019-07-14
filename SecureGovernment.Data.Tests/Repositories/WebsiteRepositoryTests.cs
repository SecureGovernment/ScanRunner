using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ScanRunner.Data.Entities;
using SecureGovernment.Data.Infastructure;
using SecureGovernment.Data.Repositories;
using SecureGovernment.Domain.Dtos;
using SecureGovernment.Domain.Interfaces.Infastructure;
using System;

namespace SecureGovernment.Data.Tests.Repositories
{
    [TestClass]
    public class WebsiteRepositoryTests : BaseTest
    {
        [TestMethod]
        public void WebsiteRepository_DoesWebsiteExist()
        {
            //Arrange
            var repository = new WebsiteRepository(new DatabaseFactory(Database));

            Database.Websites.Add(new Websites() { Id = 1, LastScan = DateTime.Today.AddDays(-10), Hostname = "uSa.gov" });
            Database.SaveChanges();

            //Act
            var doWebsitesExist = repository.DoWebsitesExist(new[] { "usa.gov", "epa.gov" });

            //Assert
            Assert.IsTrue(doWebsitesExist["usa.gov"]);
            Assert.IsFalse(doWebsitesExist["epa.gov"]);
        }

        [TestMethod]
        public void WebsiteRepository_Add()
        {
            //Arrange
            var dto = new WebsiteDto() { Id = 1032, Hostname = "usa.gov", LastScan = DateTime.Today.AddDays(-10) };

            var unitOfWork = new UnitOfWork(Database);
            var unitOfWorkFactory = new Mock<IUnitOfWorkFactory>();
            unitOfWorkFactory.Setup(x => x.GetUnitOfWork()).Returns(unitOfWork);

            var repository = new WebsiteRepository(new DatabaseFactory(Database)) { UnitOfWorkFactory = unitOfWorkFactory.Object };

            //Act
            repository.Add(dto);
            var entityToAssert = Database.Websites.Find(1032);

            //Assert
            Assert.IsNotNull(entityToAssert);
            Assert.AreEqual(1032, entityToAssert.Id);
            Assert.AreEqual("usa.gov", entityToAssert.Hostname);
            Assert.AreEqual(DateTime.Today.AddDays(-10), entityToAssert.LastScan);
        }

        [TestMethod]
        public void WebsiteRepository_GetLargestId()
        {
            //Arrange
            Database.Add(new Websites() { Id = 25, Hostname = "fcc.gov" });
            Database.Add(new Websites() { Id = 50, Hostname = "usa.gov" });
            Database.Add(new Websites() { Id = 75, Hostname = "epa.gov" });
            Database.SaveChanges();

            var repository = new WebsiteRepository(new DatabaseFactory(Database));

            //Act
            var maxId = repository.GetLargestId();

            //Assert
            Assert.AreEqual(75, maxId);
        }

        [TestMethod]
        public void WebsiteRepository_GetLargestId_NoWebsites()
        {
            //Arrange
            var repository = new WebsiteRepository(new DatabaseFactory(Database));

            //Act
            var maxId = repository.GetLargestId();

            //Assert
            Assert.AreEqual(0, maxId);
        }

        [TestMethod]
        public void WebsiteRepository_GetNextScanTargets()
        {
            //Arrange
            Database.Websites.Add(new Websites() { Id = 1, Hostname = "usa.gov", LastScan = DateTime.Today.AddDays(-234) });
            Database.Websites.Add(new Websites() { Id = 2, Hostname = "epa.gov", LastScan = DateTime.Today.AddDays(-3) });
            Database.Websites.Add(new Websites() { Id = 3, Hostname = "dhs.gov", LastScan = DateTime.Today });
            Database.Websites.Add(new Websites() { Id = 4, Hostname = "fbi.gov", LastScan = DateTime.Today.AddDays(-3243) });
            Database.Websites.Add(new Websites() { Id = 5, Hostname = "usda.gov", LastScan = DateTime.Today.AddDays(-3) });
            Database.Websites.Add(new Websites() { Id = 6, Hostname = "fda.gov", LastScan = DateTime.Today.AddDays(-193) });
            Database.SaveChanges();

            var repository = new WebsiteRepository(new DatabaseFactory(Database));

            //Act
            var websiteScanTargets = repository.GetNextScanTargets();

            //Assert
            Assert.AreEqual(6, websiteScanTargets.Count);
            Assert.AreEqual("fbi.gov", websiteScanTargets[0].Hostname);
            Assert.AreEqual("usa.gov", websiteScanTargets[1].Hostname);
            Assert.AreEqual("fda.gov", websiteScanTargets[2].Hostname);
            Assert.AreEqual("epa.gov", websiteScanTargets[3].Hostname);
            Assert.AreEqual("usda.gov", websiteScanTargets[4].Hostname);
            Assert.AreEqual("dhs.gov", websiteScanTargets[5].Hostname);
        }

        [TestMethod]
        public void WebsiteRepository_GetNextScanTargets_TargetLimit()
        {
            //Arrange
            Database.Websites.Add(new Websites() { Id = 1, Hostname = "usa.gov", LastScan = DateTime.Today.AddDays(-234) });
            Database.Websites.Add(new Websites() { Id = 2, Hostname = "epa.gov", LastScan = DateTime.Today.AddDays(-3) });
            Database.Websites.Add(new Websites() { Id = 3, Hostname = "dhs.gov", LastScan = DateTime.Today });
            Database.Websites.Add(new Websites() { Id = 4, Hostname = "fbi.gov", LastScan = DateTime.Today.AddDays(-3243) });
            Database.Websites.Add(new Websites() { Id = 5, Hostname = "usda.gov", LastScan = DateTime.Today.AddDays(-3) });
            Database.Websites.Add(new Websites() { Id = 6, Hostname = "fda.gov", LastScan = DateTime.Today.AddDays(-193) });
            Database.SaveChanges();

            var repository = new WebsiteRepository(new DatabaseFactory(Database));

            //Act
            var websiteScanTargets = repository.GetNextScanTargets(3);

            //Assert
            Assert.AreEqual(3, websiteScanTargets.Count);
            Assert.AreEqual("fbi.gov", websiteScanTargets[0]);
            Assert.AreEqual("usa.gov", websiteScanTargets[1]);
            Assert.AreEqual("fda.gov", websiteScanTargets[2]);
        }
    }
}
