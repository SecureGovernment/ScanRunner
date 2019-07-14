using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ScanRunner.Data.Entities;
using SecureGovernment.Data.Infastructure;
using SecureGovernment.Data.Repositories;
using SecureGovernment.Domain.Dtos;
using SecureGovernment.Domain.Enums;
using SecureGovernment.Domain.Interfaces.Infastructure;

namespace SecureGovernment.Data.Tests.Repositories
{
    [TestClass]
    public class WebsiteCategoryRepositoryTests : BaseTest
    {
        [TestMethod]
        public void WebsiteCategoryRepository_GetWebsiteCategory()
        {
            //Arrange
            AddWebsiteCategoryToDatabase();

            var repository = new WebsiteCategoryRepository(new DatabaseFactory(Database));

            //Act
            var websiteCategory = repository.GetWebsiteCategory(GovernmentType.FEDERAL_EXECUTIVE, "General Services AdministratioN", "Office of Citizen Services and CommunicationS", "WashingtoN", "Dc");

            //Assert
            Assert.IsNotNull(websiteCategory);
            Assert.AreEqual(websiteCategory.Id, 3691);
            Assert.AreEqual(websiteCategory.GovernmentType, websiteCategory.GovernmentType);
            Assert.AreEqual("General Services Administration", websiteCategory.Agency);
            Assert.AreEqual("Office of Citizen Services and Communications", websiteCategory.Organization);
            Assert.AreEqual("Washington", websiteCategory.City);
            Assert.AreEqual("DC", websiteCategory.State);
        }

        [TestMethod]
        public void WebsiteCategoryRepository_GetWebsiteCategory_DomainTypeMismatch()
        {
            //Arrange
            AddWebsiteCategoryToDatabase();

            var repository = new WebsiteCategoryRepository(new DatabaseFactory(Database));

            //Act
            var websiteCategory = repository.GetWebsiteCategory(GovernmentType.FEDERAL_LEGISLATIVE, "General Services AdministratioN", "Office of Citizen Services and CommunicationS", "WashingtoN", "Dc");

            //Assert
            Assert.IsNull(websiteCategory);
        }

        [TestMethod]
        public void WebsiteCategoryRepository_GetWebsiteCategory_AgencyMismatch()
        {
            //Arrange
            AddWebsiteCategoryToDatabase();

            var repository = new WebsiteCategoryRepository(new DatabaseFactory(Database));

            //Act
            var websiteCategory = repository.GetWebsiteCategory(GovernmentType.FEDERAL_EXECUTIVE, "White House", "Office of Citizen Services and CommunicationS", "WashingtoN", "Dc");

            //Assert
            Assert.IsNull(websiteCategory);
        }

        [TestMethod]
        public void WebsiteCategoryRepository_GetWebsiteCategory_OrganizationMismatch()
        {
            //Arrange
            AddWebsiteCategoryToDatabase();

            var repository = new WebsiteCategoryRepository(new DatabaseFactory(Database));

            //Act
            var websiteCategory = repository.GetWebsiteCategory(GovernmentType.FEDERAL_EXECUTIVE, "General Services AdministratioN", "General Services Administration", "WashingtoN", "Dc");

            //Assert
            Assert.IsNull(websiteCategory);
        }

        [TestMethod]
        public void WebsiteCategoryRepository_GetWebsiteCategory_CityMismatch()
        {
            //Arrange
            AddWebsiteCategoryToDatabase();

            var repository = new WebsiteCategoryRepository(new DatabaseFactory(Database));

            //Act
            var websiteCategory = repository.GetWebsiteCategory(GovernmentType.FEDERAL_EXECUTIVE, "General Services AdministratioN", "General Services Administration", "New York", "Dc");

            //Assert
            Assert.IsNull(websiteCategory);
        }

        [TestMethod]
        public void WebsiteCategoryRepository_GetWebsiteCategory_StateMismatch()
        {
            //Arrange
            AddWebsiteCategoryToDatabase();

            var repository = new WebsiteCategoryRepository(new DatabaseFactory(Database));

            //Act
            var websiteCategory = repository.GetWebsiteCategory(GovernmentType.FEDERAL_EXECUTIVE, "General Services AdministratioN", "General Services Administration", "WashingtoN", "WA");

            //Assert
            Assert.IsNull(websiteCategory);
        }

        [TestMethod]
        public void WebsiteCategoryRepository_Add()
        {
            //Arrange
            var websiteCategoryDto = new WebsiteCategoryDto() {
                Id = 7216,
                GovernmentType = (int)GovernmentType.FEDERAL_JUDICIAL,
                Agency = "The Supreme Court",
                Organization = "Supreme Court of the United States",
                City = "Washington",
                State = "DC"
            };

            var unitOfWork = new UnitOfWork(Database);
            var unitOfWorkFactory = new Mock<IUnitOfWorkFactory>();
            unitOfWorkFactory.Setup(x => x.GetUnitOfWork()).Returns(unitOfWork);

            var repository = new WebsiteCategoryRepository(new DatabaseFactory(Database)) { UnitOfWorkFactory = unitOfWorkFactory.Object };

            //Act
            repository.Add(websiteCategoryDto);
            var entityToAssert = Database.WebsiteCategories.Find(7216);

            //Assert
            Assert.IsNotNull(entityToAssert);
            Assert.AreEqual(7216, entityToAssert.Id);
            Assert.AreEqual(6, entityToAssert.GovernmentType);
            Assert.AreEqual("The Supreme Court", entityToAssert.Agency);
            Assert.AreEqual("Supreme Court of the United States", entityToAssert.Organization);
            Assert.AreEqual("DC", entityToAssert.State);
            Assert.AreEqual("Washington", entityToAssert.City);
        }

        [TestMethod]
        public void WebsiteCategoryRepository_GetLargestId()
        {
            //Arrange
            Database.Add(new WebsiteCategories() { Id = 32, Agency = "fcc.gov" });
            Database.Add(new WebsiteCategories() { Id = 65, Agency = "usa.gov" });
            Database.Add(new WebsiteCategories() { Id = 12, Agency = "epa.gov" });
            Database.SaveChanges();

            var repository = new WebsiteCategoryRepository(new DatabaseFactory(Database));

            //Act
            var maxId = repository.GetLargestId();

            //Assert
            Assert.AreEqual(65, maxId);
        }

        [TestMethod]
        public void WebsiteCategoryRepository_GetLargestId_NoCategories()
        {
            //Arrange
            var repository = new WebsiteCategoryRepository(new DatabaseFactory(Database));

            //Act
            var maxId = repository.GetLargestId();

            //Assert
            Assert.AreEqual(0, maxId);
        }

        private void AddWebsiteCategoryToDatabase()
        {
            Database.WebsiteCategories.Add(new WebsiteCategories()
            {
                Id = 3691,
                GovernmentType = (int)GovernmentType.FEDERAL_EXECUTIVE,
                Agency = "General Services Administration",
                Organization = "Office of Citizen Services and Communications",
                City = "Washington",
                State = "DC"
            });
            Database.SaveChanges();
        }
    }
}
