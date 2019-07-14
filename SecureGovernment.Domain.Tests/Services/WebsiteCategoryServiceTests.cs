using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SecureGovernment.Domain.Dtos;
using SecureGovernment.Domain.Enums;
using SecureGovernment.Domain.Models;
using SecureGovernment.Domain.Services;

namespace SecureGovernment.Domain.Tests.Services
{
    [TestClass]
    public class WebsiteCategoryServiceTests
    {
        [TestMethod]
        public void WebsiteCategoryService_GetNextId()
        {
            //Arrange
            var websiteCategoryService = Utils.Init<WebsiteCategoryService>();
            Mock.Get(websiteCategoryService.WebsiteCategoryRepository).Setup(x => x.GetLargestId()).Returns(83);

            //Act
            var nextId = websiteCategoryService.GetNextId();

            //Assert
            Assert.AreEqual(84, nextId);
        }

        [TestMethod]
        public void WebsiteCategoryService_GetOrCreateWebsiteCategory_CreateCategory()
        {
            //Arrange
            WebsiteCategoryDto dtoToAssert = null;
            var websiteEntry = DataCreationHelper.CreateStandardWebsiteEntry();

            var websiteCategoryService = Utils.Init<WebsiteCategoryService>();
            Mock.Get(websiteCategoryService.WebsiteCategoryRepository).Setup(x => x.GetLargestId()).Returns(20);
            Mock.Get(websiteCategoryService.WebsiteCategoryRepository).Setup(x => x.GetWebsiteCategory(GovernmentType.FEDERAL_EXECUTIVE, "General Services Administration", "Office of Citizen Services and Communications", "Washington", "DC")).Returns((WebsiteCategoryDto)null);
            Mock.Get(websiteCategoryService.WebsiteCategoryRepository).Setup(x => x.Add(It.IsAny<WebsiteCategoryDto>())).Callback((WebsiteCategoryDto websiteCategoryDto) =>
            {
                dtoToAssert = websiteCategoryDto;
            });

            //Act
            var websiteCategory = websiteCategoryService.GetOrCreateWebsiteCategory(websiteEntry);

            //Assert
            Assert.AreEqual(dtoToAssert, websiteCategory);
            Assert.AreEqual(21, dtoToAssert.Id);
            Assert.AreEqual("General Services Administration", dtoToAssert.Agency);
            Assert.AreEqual("Office of Citizen Services and Communications", dtoToAssert.Organization);
            Assert.AreEqual("Washington", dtoToAssert.City);
            Assert.AreEqual("DC", dtoToAssert.State);
        }

        [TestMethod]
        public void WebsiteCategoryService_GetOrCreateWebsiteCategory_GetCategory()
        {
            //Arrange
            var websiteEntry = DataCreationHelper.CreateStandardWebsiteEntry();

            var websiteCategoryService = Utils.Init<WebsiteCategoryService>();
            Mock.Get(websiteCategoryService.WebsiteCategoryRepository).Setup(x => x.GetWebsiteCategory(GovernmentType.FEDERAL_EXECUTIVE, "General Services Administration", "Office of Citizen Services and Communications", "Washington", "DC")).Returns(DataCreationHelper.CreateStandardWebsiteCategoryDto());
            Mock.Get(websiteCategoryService.WebsiteCategoryRepository).Setup(x => x.Add(It.IsAny<WebsiteCategoryDto>())).Verifiable();

            //Act
            var websiteCategory = websiteCategoryService.GetOrCreateWebsiteCategory(websiteEntry);

            //Assert
            Mock.Get(websiteCategoryService.WebsiteCategoryRepository).Verify(x => x.Add(It.IsAny<WebsiteCategoryDto>()), Times.Never);

            Assert.AreEqual(21, websiteCategory.Id);
            Assert.AreEqual("General Services Administration", websiteCategory.Agency);
            Assert.AreEqual("Office of Citizen Services and Communications", websiteCategory.Organization);
            Assert.AreEqual("Washington", websiteCategory.City);
            Assert.AreEqual("DC", websiteCategory.State);
        }
    }
}
