using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using SecureGovernment.Domain.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SecureGovernment.Domain.Tests.Services
{
    [TestClass]
    public class ScanServiceTests
    {
        [TestMethod]
        public void ScanService_TriggerScan()
        {
            //Arrange
            var service = Utils.Init<ScanService>();
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent("{ scan_id: 62710 }"),
               })
               .Verifiable();

            // use real http client with mocked handler here
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://example.com"),
            };

            ScanService.HttpClient = httpClient;
            Mock.Get(service.Settings).Setup(x => x.TlsObservatoryApiUrl).Returns("https://example.com");

            //Act
            var scanId = service.TriggerScan("usa.gov");
            scanId.Wait();

            //Assert
            Assert.AreEqual(62710, scanId.Result);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpRequestException))]
        public async Task ScanService_TriggerScan_HttpError()
        {
            //Arrange
            var service = Utils.Init<ScanService>();
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.Forbidden,
                   Content = null,
               });

            // use real http client with mocked handler here
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://example.com"),
            };

            ScanService.HttpClient = httpClient;
            Mock.Get(service.Settings).Setup(x => x.TlsObservatoryApiUrl).Returns("https://example.com");

            //Act
            var scanId = await service.TriggerScan("usa.gov");
        }
    }
}
