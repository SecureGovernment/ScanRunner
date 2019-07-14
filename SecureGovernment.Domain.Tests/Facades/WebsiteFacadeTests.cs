using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SecureGovernment.Domain.Dtos;
using SecureGovernment.Domain.Facades;
using System;
using System.Collections.Generic;

namespace SecureGovernment.Domain.Tests.Facades
{
    [TestClass]
    public class WebsiteFacadeTests
    {
        [TestMethod]
        public void WebsiteFacade_AddWebsite()
        {
            //Arrange
            WebsiteDto dtoToAssert = null;

            var websiteEntry = DataCreationHelper.CreateStandardWebsiteEntry();

            var websiteCategory = new WebsiteCategoryDto() { Id = 239 };

            var facade = Utils.Init<WebsiteFacade>();
            Mock.Get(facade.CsvService).Setup(x => x.ParseWebsiteCsvList("Fake CSV")).Returns(new[] { websiteEntry });
            Mock.Get(facade.WebsiteRepository).Setup(x => x.DoWebsitesExist(It.IsAny<List<string>>())).Returns(new Dictionary<string, bool>() { { "usa.gov", false } });
            Mock.Get(facade.WebsiteService).Setup(x => x.GetNextId()).Returns(34);
            Mock.Get(facade.WebsiteCategoryService).Setup(x => x.GetOrCreateWebsiteCategory(websiteEntry)).Returns(websiteCategory);
            Mock.Get(facade.WebsiteRepository).Setup(x => x.Add(It.IsAny<WebsiteDto>())).Callback((WebsiteDto websiteDto) =>
            {
                dtoToAssert = websiteDto;
            });

            //Act
            facade.AddWebsitesFromCsv("Fake CSV");

            //Assert
            Assert.AreEqual(34, dtoToAssert.Id);
            Assert.AreEqual("usa.gov", dtoToAssert.Hostname);
            Assert.AreEqual(DateTime.MinValue, dtoToAssert.LastScan);
            Assert.AreEqual(239, dtoToAssert.CategoryId);
        }

        [TestMethod]
        public void WebsiteFacade_AddWebsite_WebsiteAlreadyExists()
        {
            //Arrange
            var websiteEntry = DataCreationHelper.CreateStandardWebsiteEntry();

            var facade = Utils.Init<WebsiteFacade>();
            Mock.Get(facade.WebsiteRepository).Setup(x => x.DoWebsitesExist(It.IsAny<List<string>>())).Returns(new Dictionary<string, bool>() { { "usa.gov", true } });
            Mock.Get(facade.WebsiteRepository).Setup(x => x.Add(It.IsAny<WebsiteDto>())).Verifiable();
            Mock.Get(facade.CsvService).Setup(x => x.ParseWebsiteCsvList("Fake CSV")).Returns(new[] { websiteEntry });

            //Act
            facade.AddWebsitesFromCsv("Fake CSV");

            //Assert
            Mock.Get(facade.WebsiteRepository).Verify(x => x.Add(It.IsAny<WebsiteDto>()), Times.Never);
        }
    }
}
