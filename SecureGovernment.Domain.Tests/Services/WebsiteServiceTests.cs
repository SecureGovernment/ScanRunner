using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SecureGovernment.Domain.Services;

namespace SecureGovernment.Domain.Tests.Services
{
    [TestClass]
    public class WebsiteServiceTests
    {
        [TestMethod]
        public void WebsiteService_GetNextId()
        {
            //Arrange
            var service = Utils.Init<WebsiteService>();
            Mock.Get(service.WebsiteRepository).Setup(x => x.GetLargestId()).Returns(71);

            //Act
            var nextId = service.GetNextId();

            //Assert
            Assert.AreEqual(72, nextId);
        }
    }
}
